using Godot;
using System;
using System.Collections.Generic;

public partial class MessageBroker : Node
{
	public delegate void ActionRef<T>(ref T item);

	private readonly Dictionary<Type, HashSet<Delegate>> _subscribers = new();

	public void SendMessage<T>(T message)
		where T : struct
	{
		if (_subscribers.TryGetValue(typeof(T), out HashSet<Delegate>? messageSubscribers))
		{
			foreach (var messageSubscriber in messageSubscribers)
			{
				((Action<T>)messageSubscriber).Invoke(message);
			}
		}
	}

	public void SendMessageByRef<T>(ref T message)
		where T : struct
	{
		if (_subscribers.TryGetValue(typeof(T), out HashSet<Delegate>? messageSubscribers))
		{
			foreach (var messageSubscriber in messageSubscribers)
			{
				((ActionRef<T>)messageSubscriber).Invoke(ref message);
			}
		}
	}

	public void SendMessageByRef<T>(T message)
		where T : struct
	{
		if (_subscribers.TryGetValue(typeof(T), out HashSet<Delegate>? messageSubscribers))
		{
			foreach (var messageSubscriber in messageSubscribers)
			{
				((ActionRef<T>)messageSubscriber).Invoke(ref message);
			}
		}
	}

	public bool AddMessageReceiver<T>(Action<T> messageHandler)
		where T : struct
	{
		if (!_subscribers.TryGetValue(typeof(T), out HashSet<Delegate>? messageSubscribers))
		{
			messageSubscribers = new HashSet<Delegate>();
			_subscribers.Add(typeof(T), messageSubscribers);
		}

		return messageSubscribers.Add(messageHandler);
	}

	public bool AddRefMessageReceiver<T>(ActionRef<T> messageHandler)
		where T : struct
	{
		if (!_subscribers.TryGetValue(typeof(T), out HashSet<Delegate>? messageSubscribers))
		{
			messageSubscribers = new HashSet<Delegate>();
			_subscribers.Add(typeof(T), messageSubscribers);
		}

		return messageSubscribers.Add(messageHandler);
	}

	public bool RemoveMessageReceiver<T>(Action<T> messageHandler)
		where T : struct
	{
		if (_subscribers.TryGetValue(typeof(T), out HashSet<Delegate>? messageSubscribers))
		{
			return messageSubscribers.Remove(messageHandler);
		}

		return false;
	}

	public bool RemoveRefMessageReceiver<T>(ActionRef<T> messageHandler)
		where T : struct
	{
		if (_subscribers.TryGetValue(typeof(T), out HashSet<Delegate>? messageSubscribers))
		{
			return messageSubscribers.Remove(messageHandler);
		}

		return false;
	}
}
