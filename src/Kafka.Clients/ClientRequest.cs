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

using Kafka.Common.Protocol;
using Kafka.Common.Requests;

namespace Kafka.Clients;

/// <summary>
/// A request being sent to the server. This holds both the network send as well as the client-level metadata.
/// </summary>
public sealed class ClientRequest
{
    /// <summary>
    /// The brokerId to send the request to
    /// </summary>
    public string Destination { get; }

    /// <summary>
    /// The builder for the request to make
    /// </summary>
    public AbstractRequest.Builder<AbstractRequest> RequestBuilder { get; }

    /// <summary>
    /// The correlation id for this client request
    /// </summary>
    public int CorrelationId { get; }

    /// <summary>
    /// The client ID to use for the header
    /// </summary>
    public string? ClientId { get; }

    /// <summary>
    /// The unix timestamp in milliseconds for the time at which this request was created
    /// </summary>
    public long CreatedTimeMs { get; }

    /// <summary>
    /// Should we expect a response message or is this request complete once it is sent?
    /// </summary>
    public bool ExpectResponse { get; }

    /// <summary>
    /// Request timeout in milliseconds
    /// </summary>
    public int RequestTimeoutMs { get; }

    /// <summary>
    /// A callback to execute when the response has been received (or null if no callback is necessary)
    /// </summary>
    public IRequestCompletionHandler? Callback { get; }

    /// <summary>
    /// Constructs a new ClientRequest
    /// </summary>
    /// <param name="destination">The brokerId to send the request to</param>
    /// <param name="requestBuilder">The builder for the request to make</param>
    /// <param name="correlationId">The correlation id for this client request</param>
    /// <param name="clientId">The client ID to use for the header</param>
    /// <param name="createdTimeMs">The unix timestamp in milliseconds for the time at which this request was created</param>
    /// <param name="expectResponse">Should we expect a response message or is this request complete once it is sent?</param>
    /// <param name="requestTimeoutMs">Request timeout in milliseconds</param>
    /// <param name="callback">A callback to execute when the response has been received (or null if no callback is necessary)</param>
    public ClientRequest(string destination,
                        AbstractRequest.Builder<AbstractRequest> requestBuilder,
                        int correlationId,
                        string? clientId,
                        long createdTimeMs,
                        bool expectResponse,
                        int requestTimeoutMs,
                        IRequestCompletionHandler? callback)
    {
        Destination = destination;
        RequestBuilder = requestBuilder;
        CorrelationId = correlationId;
        ClientId = clientId;
        CreatedTimeMs = createdTimeMs;
        ExpectResponse = expectResponse;
        RequestTimeoutMs = requestTimeoutMs;
        Callback = callback;
    }

    public override string ToString()
    {
        return $"ClientRequest(expectResponse={ExpectResponse}, " +
               $"callback={Callback}, " +
               $"destination={Destination}, " +
               $"correlationId={CorrelationId}, " +
               $"clientId={ClientId}, " +
               $"createdTimeMs={CreatedTimeMs}, " +
               $"requestBuilder={RequestBuilder})";
    }

    public ApiKeys ApiKey => RequestBuilder.ApiKey;

    public RequestHeader MakeHeader(short version)
    {
        var requestApiKey = ApiKey;
        return new RequestHeader(
            new RequestHeaderData()
                .SetRequestApiKey((short)requestApiKey)
                .SetRequestApiVersion(version)
                .SetClientId(ClientId)
                .SetCorrelationId(CorrelationId),
            requestApiKey.RequestHeaderVersion(version));
    }
}