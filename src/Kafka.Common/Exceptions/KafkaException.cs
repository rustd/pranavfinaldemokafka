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

using System.Runtime.Serialization;

namespace Kafka.Common.Exceptions;

/// <summary>
/// Base exception for all Kafka-related errors
/// </summary>
[Serializable]
public class KafkaException : Exception
{
    public KafkaException()
    {
    }

    public KafkaException(string message) : base(message)
    {
    }

    public KafkaException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected KafkaException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

/// <summary>
/// Exception thrown when authentication fails
/// </summary>
[Serializable]
public class AuthenticationException : KafkaException
{
    public AuthenticationException()
    {
    }

    public AuthenticationException(string message) : base(message)
    {
    }

    public AuthenticationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected AuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

/// <summary>
/// Exception thrown when an unsupported version is encountered
/// </summary>
[Serializable]
public class UnsupportedVersionException : KafkaException
{
    public UnsupportedVersionException()
    {
    }

    public UnsupportedVersionException(string message) : base(message)
    {
    }

    public UnsupportedVersionException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected UnsupportedVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

/// <summary>
/// Exception thrown when a producer has been fenced
/// </summary>
[Serializable]
public class ProducerFencedException : KafkaException
{
    public ProducerFencedException()
    {
    }

    public ProducerFencedException(string message) : base(message)
    {
    }

    public ProducerFencedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ProducerFencedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}