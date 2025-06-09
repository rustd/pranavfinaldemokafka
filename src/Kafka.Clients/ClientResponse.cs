/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Kafka.Common.Exceptions;
using Kafka.Common.Requests;

namespace Kafka.Clients;

/// <summary>
/// A response from the server. Contains both the body of the response as well as the correlated request
/// metadata that was originally sent.
/// </summary>
public class ClientResponse
{
    /// <summary>
    /// The header of the corresponding request
    /// </summary>
    public RequestHeader RequestHeader { get; }

    /// <summary>
    /// The callback to be invoked
    /// </summary>
    public IRequestCompletionHandler? Callback { get; }

    /// <summary>
    /// The node the corresponding request was sent to
    /// </summary>
    public string Destination { get; }

    /// <summary>
    /// The unix timestamp when this response was received
    /// </summary>
    public long ReceivedTimeMs { get; }

    /// <summary>
    /// The latency of the request in milliseconds
    /// </summary>
    public long LatencyMs { get; }

    /// <summary>
    /// Whether the client disconnected before fully reading a response
    /// </summary>
    public bool Disconnected { get; }

    /// <summary>
    /// Whether there was a version mismatch that prevented sending the request
    /// </summary>
    public UnsupportedVersionException? VersionMismatch { get; }

    /// <summary>
    /// Authentication exception if authentication failed
    /// </summary>
    public AuthenticationException? AuthenticationException { get; }

    /// <summary>
    /// The response contents (or null) if we disconnected, no response was expected,
    /// or if there was a version mismatch
    /// </summary>
    public AbstractResponse? ResponseBody { get; }

    /// <summary>
    /// Constructs a new ClientResponse
    /// </summary>
    /// <param name="requestHeader">The header of the corresponding request</param>
    /// <param name="callback">The callback to be invoked</param>
    /// <param name="destination">The node the corresponding request was sent to</param>
    /// <param name="createdTimeMs">The unix timestamp when the corresponding request was created</param>
    /// <param name="receivedTimeMs">The unix timestamp when this response was received</param>
    /// <param name="disconnected">Whether the client disconnected before fully reading a response</param>
    /// <param name="versionMismatch">Whether there was a version mismatch that prevented sending the request</param>
    /// <param name="authenticationException">Authentication exception if authentication failed</param>
    /// <param name="responseBody">The response contents (or null) if we disconnected, no response was expected, or if there was a version mismatch</param>
    public ClientResponse(RequestHeader requestHeader,
                         IRequestCompletionHandler? callback,
                         string destination,
                         long createdTimeMs,
                         long receivedTimeMs,
                         bool disconnected,
                         UnsupportedVersionException? versionMismatch,
                         AuthenticationException? authenticationException,
                         AbstractResponse? responseBody)
    {
        RequestHeader = requestHeader;
        Callback = callback;
        Destination = destination;
        ReceivedTimeMs = receivedTimeMs;
        LatencyMs = receivedTimeMs - createdTimeMs;
        Disconnected = disconnected;
        VersionMismatch = versionMismatch;
        AuthenticationException = authenticationException;
        ResponseBody = responseBody;
    }

    /// <summary>
    /// Returns true if the client disconnected before receiving a response
    /// </summary>
    public bool WasDisconnected => Disconnected;

    /// <summary>
    /// Returns true if there is a response body
    /// </summary>
    public bool HasResponse => ResponseBody != null;

    /// <summary>
    /// Invokes the completion callback if present
    /// </summary>
    public void OnComplete()
    {
        Callback?.OnComplete(this);
    }

    public override string ToString()
    {
        return $"ClientResponse(receivedTimeMs={ReceivedTimeMs}, " +
               $"latencyMs={LatencyMs}, " +
               $"disconnected={Disconnected}, " +
               $"requestHeader={RequestHeader}, " +
               $"responseBody={ResponseBody})";
    }
}