
// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System;
using System.Collections.Generic;
using Improbable;
using Improbable.Gdk.Core;
using Improbable.Gdk.GameObjectRepresentation;
using Improbable.Worker;
using Unity.Entities;
using UnityEngine;

namespace Improbable.Gdk.Tests.NonblittableTypes
{
    public partial class NonBlittableComponent
    {
        public partial class FirstCommand
        {
            public struct RequestResponder
            {
                private readonly EntityManager entityManager;
                private readonly Entity entity;
                public FirstCommand.ReceivedRequest Request { get; }

                internal RequestResponder(EntityManager entityManager, Entity entity, FirstCommand.ReceivedRequest request)
                {
                    this.entity = entity;
                    this.entityManager = entityManager;
                    Request = request;
                }

                public void SendResponse(global::Improbable.Gdk.Tests.NonblittableTypes.FirstCommandResponse payload)
                {
                    entityManager.GetComponentData<CommandResponders.FirstCommand>(entity).ResponsesToSend
                        .Add(FirstCommand.CreateResponse(Request, payload));
                }

                public void SendResponseFailure(string message)
                {
                    entityManager.GetComponentData<CommandResponders.FirstCommand>(entity).ResponsesToSend
                        .Add(FirstCommand.CreateResponseFailure(Request, message));
                }
            }
        }

        public partial class SecondCommand
        {
            public struct RequestResponder
            {
                private readonly EntityManager entityManager;
                private readonly Entity entity;
                public SecondCommand.ReceivedRequest Request { get; }

                internal RequestResponder(EntityManager entityManager, Entity entity, SecondCommand.ReceivedRequest request)
                {
                    this.entity = entity;
                    this.entityManager = entityManager;
                    Request = request;
                }

                public void SendResponse(global::Improbable.Gdk.Tests.NonblittableTypes.SecondCommandResponse payload)
                {
                    entityManager.GetComponentData<CommandResponders.SecondCommand>(entity).ResponsesToSend
                        .Add(SecondCommand.CreateResponse(Request, payload));
                }

                public void SendResponseFailure(string message)
                {
                    entityManager.GetComponentData<CommandResponders.SecondCommand>(entity).ResponsesToSend
                        .Add(SecondCommand.CreateResponseFailure(Request, message));
                }
            }
        }

        public partial class Requirable
        {
            [InjectableId(InjectableType.CommandRequestSender, 1002)]
            internal class CommandRequestSenderCreator : IInjectableCreator
            {
                public IInjectable CreateInjectable(Entity entity, EntityManager entityManager, ILogDispatcher logDispatcher)
                {
                    return new CommandRequestSender(entity, entityManager, logDispatcher);
                }
            }

            [InjectableId(InjectableType.CommandRequestSender, 1002)]
            [InjectionCondition(InjectionCondition.RequireNothing)]
            public class CommandRequestSender : RequirableBase, ICommandRequestSender
            {
                private Entity entity;
                private readonly EntityManager entityManager;
                private readonly ILogDispatcher logger;

                public CommandRequestSender(Entity entity, EntityManager entityManager, ILogDispatcher logger) : base(logger)
                {
                    this.entity = entity;
                    this.entityManager = entityManager;
                    this.logger = logger;
                }

                public CommandTypeInformation<FirstCommand, global::Improbable.Gdk.Tests.NonblittableTypes.FirstCommandRequest, FirstCommand.ReceivedResponse> FirstCommandTypeInformation;

                public long SendFirstCommandRequest(EntityId entityId, global::Improbable.Gdk.Tests.NonblittableTypes.FirstCommandRequest payload,
                    uint? timeoutMillis = null, bool allowShortCircuiting = false, object context = null)
                {
                    if (!IsValid())
                    {
                        return -1;
                    }

                    var ecsCommandRequestSender = entityManager.GetComponentData<CommandSenders.FirstCommand>(entity);
                    var request = FirstCommand.CreateRequest(entityId, payload, timeoutMillis, allowShortCircuiting, context);
                    ecsCommandRequestSender.RequestsToSend.Add(request);
                    return request.RequestId;
                }

                public long SendFirstCommandRequest(EntityId entityId, global::Improbable.Gdk.Tests.NonblittableTypes.FirstCommandRequest payload,
                    Action<FirstCommand.ReceivedResponse> callback, uint? timeoutMillis = null, bool allowShortCircuiting = false)
                {
                    if (!IsValid())
                    {
                        return -1;
                    }

                    Action<FirstCommand.ReceivedResponse> wrappedCallback = response =>
                    {
                        if (this.IsValid() && callback != null)
                        {
                            callback(response);
                        }
                    };

                    var ecsCommandRequestSender = entityManager.GetComponentData<CommandSenders.FirstCommand>(entity);
                    var request = FirstCommand.CreateRequest(entityId, payload, timeoutMillis, allowShortCircuiting, callback);
                    ecsCommandRequestSender.RequestsToSend.Add(request);
                    return request.RequestId;
                }

                public CommandTypeInformation<SecondCommand, global::Improbable.Gdk.Tests.NonblittableTypes.SecondCommandRequest, SecondCommand.ReceivedResponse> SecondCommandTypeInformation;

                public long SendSecondCommandRequest(EntityId entityId, global::Improbable.Gdk.Tests.NonblittableTypes.SecondCommandRequest payload,
                    uint? timeoutMillis = null, bool allowShortCircuiting = false, object context = null)
                {
                    if (!IsValid())
                    {
                        return -1;
                    }

                    var ecsCommandRequestSender = entityManager.GetComponentData<CommandSenders.SecondCommand>(entity);
                    var request = SecondCommand.CreateRequest(entityId, payload, timeoutMillis, allowShortCircuiting, context);
                    ecsCommandRequestSender.RequestsToSend.Add(request);
                    return request.RequestId;
                }

                public long SendSecondCommandRequest(EntityId entityId, global::Improbable.Gdk.Tests.NonblittableTypes.SecondCommandRequest payload,
                    Action<SecondCommand.ReceivedResponse> callback, uint? timeoutMillis = null, bool allowShortCircuiting = false)
                {
                    if (!IsValid())
                    {
                        return -1;
                    }

                    Action<SecondCommand.ReceivedResponse> wrappedCallback = response =>
                    {
                        if (this.IsValid() && callback != null)
                        {
                            callback(response);
                        }
                    };

                    var ecsCommandRequestSender = entityManager.GetComponentData<CommandSenders.SecondCommand>(entity);
                    var request = SecondCommand.CreateRequest(entityId, payload, timeoutMillis, allowShortCircuiting, callback);
                    ecsCommandRequestSender.RequestsToSend.Add(request);
                    return request.RequestId;
                }

                long ICommandRequestSender.SendCommand<TCommand, TRequest, TResponse>(CommandTypeInformation<TCommand, TRequest, TResponse> commandTypeInformation, EntityId entityId, TRequest request,
                    Action<TResponse> callback, uint? timeoutMillis = null, bool allowShortCircuiting = false)
                {
                    if (typeof(TCommand) == typeof(FirstCommand))
                    {
                        if (callback != null && !(typeof(TResponse) == typeof(FirstCommand.ReceivedResponse)))
                        {
                            throw new ArgumentException(
                                $"Callback for command {nameof(FirstCommand)} must be an Action taking type {typeof(FirstCommand.ReceivedResponse).FullName}");
                        }

                        // Can not directly cast to a struct and can not use unsafe code as the request type can not be constrained using unmanaged
                        switch (request)
                        {
                            case global::Improbable.Gdk.Tests.NonblittableTypes.FirstCommandRequest concreteRequest:
                            {
                                var concreteCallback = callback as Action<FirstCommand.ReceivedResponse>;
                                return SendFirstCommandRequest(entityId, concreteRequest, concreteCallback, timeoutMillis,
                                    allowShortCircuiting);
                            }
                            default:
                                throw new ArgumentException(
                                    $"Request payload for command FirstCommand, must be of type {nameof(global::Improbable.Gdk.Tests.NonblittableTypes.FirstCommandRequest)}");
                        }
                    }

                    if (typeof(TCommand) == typeof(SecondCommand))
                    {
                        if (callback != null && !(typeof(TResponse) == typeof(SecondCommand.ReceivedResponse)))
                        {
                            throw new ArgumentException(
                                $"Callback for command {nameof(SecondCommand)} must be an Action taking type {typeof(SecondCommand.ReceivedResponse).FullName}");
                        }

                        // Can not directly cast to a struct and can not use unsafe code as the request type can not be constrained using unmanaged
                        switch (request)
                        {
                            case global::Improbable.Gdk.Tests.NonblittableTypes.SecondCommandRequest concreteRequest:
                            {
                                var concreteCallback = callback as Action<SecondCommand.ReceivedResponse>;
                                return SendSecondCommandRequest(entityId, concreteRequest, concreteCallback, timeoutMillis,
                                    allowShortCircuiting);
                            }
                            default:
                                throw new ArgumentException(
                                    $"Request payload for command SecondCommand, must be of type {nameof(global::Improbable.Gdk.Tests.NonblittableTypes.SecondCommandRequest)}");
                        }
                    }


                    throw new ArgumentException($"Can not send unknown command {typeof(TRequest)}");
                }
            }

            [InjectableId(InjectableType.CommandRequestHandler, 1002)]
            internal class CommandRequestHandlerCreator : IInjectableCreator
            {
                public IInjectable CreateInjectable(Entity entity, EntityManager entityManager, ILogDispatcher logDispatcher)
                {
                    return new CommandRequestHandler(entity, entityManager, logDispatcher);
                }
            }

            [InjectableId(InjectableType.CommandRequestHandler, 1002)]
            [InjectionCondition(InjectionCondition.RequireComponentWithAuthority)]
            public class CommandRequestHandler : RequirableBase
            {
                private Entity entity;
                private readonly EntityManager entityManager;
                private readonly ILogDispatcher logger;

                public CommandRequestHandler(Entity entity, EntityManager entityManager, ILogDispatcher logger) : base(logger)
                {
                    this.entity = entity;
                    this.entityManager = entityManager;
                    this.logger = logger;
                }
                private readonly List<Action<FirstCommand.RequestResponder>> firstCommandDelegates = new List<Action<FirstCommand.RequestResponder>>();
                public event Action<FirstCommand.RequestResponder> OnFirstCommandRequest
                {
                    add
                    {
                        if (!IsValid())
                        {
                            return;
                        }

                        firstCommandDelegates.Add(value);
                    }
                    remove
                    {
                        if (!IsValid())
                        {
                            return;
                        }

                        firstCommandDelegates.Remove(value);
                    }
                }

                internal void OnFirstCommandRequestInternal(FirstCommand.ReceivedRequest request)
                {
                    GameObjectDelegates.DispatchWithErrorHandling(new FirstCommand.RequestResponder(entityManager, entity, request), firstCommandDelegates, logger);
                }
                private readonly List<Action<SecondCommand.RequestResponder>> secondCommandDelegates = new List<Action<SecondCommand.RequestResponder>>();
                public event Action<SecondCommand.RequestResponder> OnSecondCommandRequest
                {
                    add
                    {
                        if (!IsValid())
                        {
                            return;
                        }

                        secondCommandDelegates.Add(value);
                    }
                    remove
                    {
                        if (!IsValid())
                        {
                            return;
                        }

                        secondCommandDelegates.Remove(value);
                    }
                }

                internal void OnSecondCommandRequestInternal(SecondCommand.ReceivedRequest request)
                {
                    GameObjectDelegates.DispatchWithErrorHandling(new SecondCommand.RequestResponder(entityManager, entity, request), secondCommandDelegates, logger);
                }
            }

            [InjectableId(InjectableType.CommandResponseHandler, 1002)]
            internal class CommandResponseHandlerCreator : IInjectableCreator
            {
                public IInjectable CreateInjectable(Entity entity, EntityManager entityManager, ILogDispatcher logDispatcher)
                {
                    return new CommandResponseHandler(entity, entityManager, logDispatcher);
                }
            }

            [InjectableId(InjectableType.CommandResponseHandler, 1002)]
            [InjectionCondition(InjectionCondition.RequireNothing)]
            public class CommandResponseHandler : RequirableBase
            {
                private Entity entity;
                private readonly EntityManager entityManager;
                private readonly ILogDispatcher logger;

                public CommandResponseHandler(Entity entity, EntityManager entityManager, ILogDispatcher logger) : base(logger)
                {
                    this.entity = entity;
                    this.entityManager = entityManager;
                    this.logger = logger;
                }

                private readonly List<Action<FirstCommand.ReceivedResponse>> firstCommandDelegates = new List<Action<FirstCommand.ReceivedResponse>>();
                public event Action<FirstCommand.ReceivedResponse> OnFirstCommandResponse
                {
                    add
                    {
                        if (!IsValid())
                        {
                            return;
                        }

                        firstCommandDelegates.Add(value);
                    }
                    remove
                    {
                        if (!IsValid())
                        {
                            return;
                        }

                        firstCommandDelegates.Remove(value);
                    }
                }

                internal void OnFirstCommandResponseInternal(FirstCommand.ReceivedResponse response)
                {
                    GameObjectDelegates.DispatchWithErrorHandling(response, firstCommandDelegates, logger);
                }

                private readonly List<Action<SecondCommand.ReceivedResponse>> secondCommandDelegates = new List<Action<SecondCommand.ReceivedResponse>>();
                public event Action<SecondCommand.ReceivedResponse> OnSecondCommandResponse
                {
                    add
                    {
                        if (!IsValid())
                        {
                            return;
                        }

                        secondCommandDelegates.Add(value);
                    }
                    remove
                    {
                        if (!IsValid())
                        {
                            return;
                        }

                        secondCommandDelegates.Remove(value);
                    }
                }

                internal void OnSecondCommandResponseInternal(SecondCommand.ReceivedResponse response)
                {
                    GameObjectDelegates.DispatchWithErrorHandling(response, secondCommandDelegates, logger);
                }
            }
        }
    }
}
