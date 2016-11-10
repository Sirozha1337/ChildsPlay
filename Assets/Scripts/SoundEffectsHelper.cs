﻿using UnityEngine;
using System.Collections;

public class SoundEffectsHelper : MonoBehaviour {
	// Add sound effects
	public AudioClip[] jumpingSound;
	public AudioClip[] fallFromJumpSound;
	public AudioClip movingSound;
	public AudioClip musicTheme;

	// Control the volume 
	public float volumeJumpFall;
	public float volumeTheme;

	// Play sounds
	private AudioSource characterJumpFall;
	private AudioSource characterMove;
	private AudioSource theme;

	// This is for checking if player is moving
	private GameObject player;

	// Returns a new audiosource
	AudioSource AddAudio(bool loop, bool playAwake, float vol) { 
     	AudioSource newAudio = gameObject.AddComponent<AudioSource>();
     	newAudio.loop = loop;
     	newAudio.playOnAwake = playAwake;
     	newAudio.volume = vol; 
     	return newAudio; 
	}

	// Use this for initialization
	void Start () {
		// Initialize Jump and Fall source
		characterJumpFall = AddAudio(false, true, 0.5f);
		
		// Initialize Movement sound source
		characterMove = AddAudio(true, true, 0.5f);
		characterMove.clip = movingSound;

		// Initialize music theme source
		theme = AddAudio(true, true, volumeTheme);
		theme.clip = musicTheme;
		theme.Play();

		// Find the player gameObject
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		// Play moving sound 
		MovingSound();
	}

	void JumpingSound(){
		// If it's not Game Over play a sound
		if(!GameOverScript.IsGameOver){
			characterJumpFall.PlayOneShot(jumpingSound[Random.Range(0, jumpingSound.Length)], volumeJumpFall);
		}
	}

	void FallingSound(){
		// If it's not Game Over play a sound
		if(!GameOverScript.IsGameOver){
			characterJumpFall.PlayOneShot(fallFromJumpSound[Random.Range(0, fallFromJumpSound.Length)], volumeJumpFall);
		}
	}

	void MovingSound(){
		// Check if player is moving and play movement sound
		if(player.GetComponent<Rigidbody2D>().velocity.x > 0 || player.GetComponent<Rigidbody2D>().velocity.x < 0  &&   player.GetComponent<PlayerScript>().isGrounded && !GameOverScript.IsGameOver){
			if(!characterMove.isPlaying){
				characterMove.Play();
			}
		}else{
			characterMove.Pause();
		}
	}
}
