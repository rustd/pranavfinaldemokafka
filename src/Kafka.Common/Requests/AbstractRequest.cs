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

namespace Kafka.Common.Requests;

/// <summary>
/// Base class for all Kafka requests
/// </summary>
public abstract class AbstractRequest
{
    public abstract ApiKeys ApiKey { get; }

    /// <summary>
    /// Base builder class for creating requests
    /// </summary>
    public abstract class Builder<T> where T : AbstractRequest
    {
        public abstract ApiKeys ApiKey { get; }
        public abstract T Build(short version);
    }
}

/// <summary>
/// Base class for all Kafka responses
/// </summary>
public abstract class AbstractResponse
{
    public abstract int ThrottleTimeMs { get; }
}

/// <summary>
/// Request header data
/// </summary>
public class RequestHeaderData
{
    public short RequestApiKey { get; set; }
    public short RequestApiVersion { get; set; }
    public string? ClientId { get; set; }
    public int CorrelationId { get; set; }

    public RequestHeaderData SetRequestApiKey(short requestApiKey)
    {
        RequestApiKey = requestApiKey;
        return this;
    }

    public RequestHeaderData SetRequestApiVersion(short requestApiVersion)
    {
        RequestApiVersion = requestApiVersion;
        return this;
    }

    public RequestHeaderData SetClientId(string? clientId)
    {
        ClientId = clientId;
        return this;
    }

    public RequestHeaderData SetCorrelationId(int correlationId)
    {
        CorrelationId = correlationId;
        return this;
    }
}

/// <summary>
/// Request header
/// </summary>
public class RequestHeader
{
    public RequestHeaderData Data { get; }
    public short HeaderVersion { get; }

    public RequestHeader(RequestHeaderData data, short headerVersion)
    {
        Data = data;
        HeaderVersion = headerVersion;
    }

    public override string ToString()
    {
        return $"RequestHeader(apiKey={Data.RequestApiKey}, apiVersion={Data.RequestApiVersion}, " +
               $"clientId={Data.ClientId}, correlationId={Data.CorrelationId})";
    }
}