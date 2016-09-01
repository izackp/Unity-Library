using UnityEngine;
using System.Collections;

/// IEnumerator MyAwesomeTask()
/// {
///     while(true) {
///         Debug.Log("Logcat iz in ur consolez, spammin u wif messagez.");
///         yield return null;
////    }
/// }
///
/// IEnumerator TaskKiller(float delay, CoroutineTask t)
/// {
///     yield return new WaitForSeconds(delay);
///     t.Stop();
/// }
///
/// void SomeCodeThatCouldBeAnywhereInTheUniverse()
/// {
///     CoroutineTask spam = new CoroutineTask(MyAwesomeTask());
///     new CoroutineTask(TaskKiller(5, spam));
/// }

public class CoroutineTask
{
	public bool Running {
		get {
			return task.Running;
		}
	}

	public bool Paused {
		get {
			return task.Paused;
		}
	}

	public delegate void FinishedHandler(bool manual);

	public event FinishedHandler Finished;
	
	public CoroutineTask(IEnumerator c, bool autoStart = true)
	{
		task = TaskManager.CreateTask(c);
		task.Finished += TaskFinished;
		if (autoStart)
			Start();
	}

	public void Start()
	{
		task.Start();
	}

	public void Stop()
	{
		task.Stop();
	}
	
	public void Pause()
	{
		task.Pause();
	}
	
	public void Unpause()
	{
		task.Unpause();
	}
	
	void TaskFinished(bool manual)
	{
		FinishedHandler handler = Finished;
		if (handler != null)
			handler(manual);
	}
	
	TaskManager.TaskState task;
}

class TaskManager : MonoBehaviour
{
	public class TaskState
	{
		public bool Running {
			get {
				return running;
			}
		}

		public bool Paused  {
			get {
				return paused;
			}
		}

		public delegate void FinishedHandler(bool manual);
		public event FinishedHandler Finished;

		IEnumerator coroutine;
		bool running;
		bool paused;
		bool stopped;
		
		public TaskState(IEnumerator c)
		{
			coroutine = c;
		}
		
		public void Pause()
		{
			paused = true;
		}
		
		public void Unpause()
		{
			paused = false;
		}
		
		public void Start()
		{
			running = true;
			singleton.StartCoroutine(CallWrapper());
		}
		
		public void Stop()
		{
			stopped = true;
			running = false;
		}
		
		IEnumerator CallWrapper()
		{
			yield return null;
			IEnumerator e = coroutine;
			while(running) {
				if(paused)
					yield return null;
				else {
					if(e != null && e.MoveNext()) {
						yield return e.Current;
					}
					else {
						running = false;
					}
				}
			}
			
			FinishedHandler handler = Finished;
			if(handler != null)
				handler(stopped);
		}
	}

	static TaskManager singleton;

	public static TaskState CreateTask(IEnumerator coroutine)
	{
		if (singleton == null) {
			GameObject go = new GameObject("TaskManager");
			singleton = go.AddComponent<TaskManager>();
		}
		return new TaskState(coroutine);
	}
}
