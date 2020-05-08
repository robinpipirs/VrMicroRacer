﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleBehaviour
{
    [RequireComponent(typeof(CarControllerSystem))]
    [RequireComponent(typeof(AudioSource))]

    public class EngineSoundController : MonoBehaviour
    {

        [Header("AudioClips")]
        public AudioClip starting;
        public AudioClip rolling;
        public AudioClip stopping;

        [Header("pitch parameter")]
        public float flatoutSpeed = 20.0f;
        [Range(0.0f, 3.0f)]
        public float minPitch = 0.7f;
        [Range(0.0f, 0.1f)]
        public float pitchSpeed = 0.05f;

        private AudioSource _source;
        private CarControllerSystem _vehicle;

        void Start()
        {
            _source = GetComponent<AudioSource>();
            _vehicle = GetComponent<CarControllerSystem>();
        }

        void Update()
        {
            if (_vehicle.Handbrake && _source.clip == rolling)
            {
                _source.clip = stopping;
                _source.Play();
            }

            if (!_vehicle.Handbrake && (_source.clip == stopping || _source.clip == null))
            {
                _source.clip = starting;
                _source.Play();

                _source.pitch = 1;
            }

            if (!_vehicle.Handbrake && !_source.isPlaying)
            {
                _source.clip = rolling;
                _source.Play();
            }

            if (_source.clip == rolling)
            {
                _source.pitch = Mathf.Lerp(_source.pitch, minPitch + Mathf.Abs(_vehicle.Speed) / flatoutSpeed, pitchSpeed);
            }
        }
    }
}