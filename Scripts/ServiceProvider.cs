using Godot;
using System;

public partial class ServiceProvider : SingletonNode<ServiceProvider>
{
	private System.Collections.Generic.Dictionary<Type, object> _serviceCollection = new();


	public T GetService<T>()
	{
		if (_serviceCollection.TryGetValue(typeof(T), out var service))
		{
			return (T)service!;
		}

		throw new Exception($"Could not find service: {nameof(T)}");
	}

	public bool TryGetService<T>(out T? service)
	{
		if (_serviceCollection.TryGetValue(typeof(T), out var s))
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

		if (!_serviceCollection.TryAdd(typeof(T), service))
			throw new Exception($"Service \"{nameof(T)}\" has already been added.");
	}

	public bool TryAddService<T>(T service)
	{
		ArgumentNullException.ThrowIfNull(service, nameof(service));

		return _serviceCollection.TryAdd(typeof(T), service);
	}

	public void RemoveService<T>()
	{
		if (!_serviceCollection.Remove(typeof(T)))
			throw new Exception($"Service \"{nameof(T)}\" is not provided.");
	}

	public bool TryRemoveService<T>()
	{
		return _serviceCollection.Remove(typeof(T));
	}
}
