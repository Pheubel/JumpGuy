using Godot;
using System;

public partial class ServiceProvider : Node
{
	private System.Collections.Generic.Dictionary<Type, object> _service = new();

	public static ServiceProvider Instance { get; private set; } = null!;

	public override void _Ready()
	{
		if (Instance is null)
			Instance = this;
		else
			throw new Exception("Service provider is already instantiated, for correct usage, be sure to add it to the auto-load and let it handle the rest.");
	}

	public T GetService<T>()
	{
		if (_service.TryGetValue(typeof(T), out var service))
		{
			return (T)service!;
		}

		throw new Exception($"Could not find service: {nameof(T)}");
	}

	public bool TryGetService<T>(out T? service)
	{
		if (_service.TryGetValue(typeof(T), out var s))
		{
			service = (T)s!;
			return true;
		}

		service = default;
		return false;
	}

	public void AddService<T>(T service)
	{
		ArgumentNullException.ThrowIfNull(service, nameof(service));

		if (!_service.TryAdd(typeof(T), service))
			throw new Exception($"Service \"{nameof(T)}\" has already been added.");
	}

	public bool TryAddService<T>(T service)
	{
		ArgumentNullException.ThrowIfNull(service, nameof(service));

		return _service.TryAdd(typeof(T), service);
	}

	public void RemoveService<T>()
	{
		if (!_service.Remove(typeof(T)))
			throw new Exception($"Service \"{nameof(T)}\" is not provided.");
	}

	public bool TryRemoveService<T>()
	{
		return _service.Remove(typeof(T));
	}
}
