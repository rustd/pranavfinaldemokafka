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

using Kafka.Clients;
using Kafka.Common.Protocol;
using Kafka.Common.Requests;
using Xunit;

namespace Kafka.Tests;

public class ClientRequestTests
{
    private class TestRequestBuilder : AbstractRequest.Builder<AbstractRequest>
    {
        public override ApiKeys ApiKey => ApiKeys.Metadata;

        public override AbstractRequest Build(short version)
        {
            return new TestRequest();
        }
    }

    private class TestRequest : AbstractRequest
    {
        public override ApiKeys ApiKey => ApiKeys.Metadata;
    }

    [Fact]
    public void ClientRequest_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var destination = "broker1";
        var requestBuilder = new TestRequestBuilder();
        var correlationId = 12345;
        var clientId = "test-client";
        var createdTimeMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var expectResponse = true;
        var requestTimeoutMs = 30000;
        IRequestCompletionHandler? callback = null;

        // Act
        var request = new ClientRequest(
            destination,
            requestBuilder,
            correlationId,
            clientId,
            createdTimeMs,
            expectResponse,
            requestTimeoutMs,
            callback);

        // Assert
        Assert.Equal(destination, request.Destination);
        Assert.Equal(requestBuilder, request.RequestBuilder);
        Assert.Equal(correlationId, request.CorrelationId);
        Assert.Equal(clientId, request.ClientId);
        Assert.Equal(createdTimeMs, request.CreatedTimeMs);
        Assert.Equal(expectResponse, request.ExpectResponse);
        Assert.Equal(requestTimeoutMs, request.RequestTimeoutMs);
        Assert.Equal(callback, request.Callback);
        Assert.Equal(ApiKeys.Metadata, request.ApiKey);
    }

    [Fact]
    public void ClientRequest_MakeHeader_CreatesCorrectHeader()
    {
        // Arrange
        var destination = "broker1";
        var requestBuilder = new TestRequestBuilder();
        var correlationId = 12345;
        var clientId = "test-client";
        var createdTimeMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var expectResponse = true;
        var requestTimeoutMs = 30000;

        var request = new ClientRequest(
            destination,
            requestBuilder,
            correlationId,
            clientId,
            createdTimeMs,
            expectResponse,
            requestTimeoutMs,
            null);

        // Act
        var header = request.MakeHeader(2);

        // Assert
        Assert.Equal((short)ApiKeys.Metadata, header.Data.RequestApiKey);
        Assert.Equal(2, header.Data.RequestApiVersion);
        Assert.Equal(clientId, header.Data.ClientId);
        Assert.Equal(correlationId, header.Data.CorrelationId);
    }

    [Fact]
    public void ClientRequest_ToString_ReturnsExpectedFormat()
    {
        // Arrange
        var destination = "broker1";
        var requestBuilder = new TestRequestBuilder();
        var correlationId = 12345;
        var clientId = "test-client";
        var createdTimeMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var expectResponse = true;
        var requestTimeoutMs = 30000;

        var request = new ClientRequest(
            destination,
            requestBuilder,
            correlationId,
            clientId,
            createdTimeMs,
            expectResponse,
            requestTimeoutMs,
            null);

        // Act
        var result = request.ToString();

        // Assert
        Assert.Contains("ClientRequest", result);
        Assert.Contains($"expectResponse={expectResponse}", result);
        Assert.Contains($"destination={destination}", result);
        Assert.Contains($"correlationId={correlationId}", result);
        Assert.Contains($"clientId={clientId}", result);
    }
}