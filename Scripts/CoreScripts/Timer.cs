using UnityEngine;
using System.Collections;

public class Timer
{
	private int timer;
	private int cooldown;
	private bool once;
	private int pauseTimer=0;
	
	public Timer (int _timer, bool _once)
	{
		cooldown = _timer;
		timer = _timer;
		once = _once;
	}

	public bool Ready
	{
		get 
		{
            Update();
			return (timer<=0); 
		}
		private set 
		{ 
		
		}
	}

	public void Pause(int _value)
	{
		pauseTimer += _value;
	}

	public void Update()
	{
		if (pauseTimer <= 0)
		{
			if (timer > 0)
			{
				timer--;
			}
			else
			{
				if (!once)
					timer = cooldown;
			}
		}
		else
		{
			pauseTimer--;
		}
	}
}


