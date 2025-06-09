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

namespace Kafka.Clients.Producer;

/// <summary>
/// The interface for Kafka producers
/// </summary>
/// <typeparam name="TKey">Type of the record key</typeparam>
/// <typeparam name="TValue">Type of the record value</typeparam>
public interface IProducer<TKey, TValue> : IDisposable
{
    /// <summary>
    /// Initialize transactions
    /// </summary>
    void InitTransactions();

    /// <summary>
    /// Begin a new transaction
    /// </summary>
    /// <exception cref="ProducerFencedException">Thrown if the producer has been fenced</exception>
    void BeginTransaction();

    /// <summary>
    /// Commit the current transaction
    /// </summary>
    /// <exception cref="ProducerFencedException">Thrown if the producer has been fenced</exception>
    void CommitTransaction();

    /// <summary>
    /// Abort the current transaction
    /// </summary>
    /// <exception cref="ProducerFencedException">Thrown if the producer has been fenced</exception>
    void AbortTransaction();

    /// <summary>
    /// Send a record asynchronously
    /// </summary>
    /// <param name="record">The record to send</param>
    /// <returns>A task representing the send operation</returns>
    Task<RecordMetadata> SendAsync(ProducerRecord<TKey, TValue> record);

    /// <summary>
    /// Send a record asynchronously with a callback
    /// </summary>
    /// <param name="record">The record to send</param>
    /// <param name="callback">Callback to execute when the send completes</param>
    /// <returns>A task representing the send operation</returns>
    Task<RecordMetadata> SendAsync(ProducerRecord<TKey, TValue> record, Action<RecordMetadata?, Exception?> callback);

    /// <summary>
    /// Flush all pending sends
    /// </summary>
    Task FlushAsync();

    /// <summary>
    /// Close the producer
    /// </summary>
    /// <param name="timeout">Timeout for closing</param>
    Task CloseAsync(TimeSpan timeout);
}

/// <summary>
/// A record to be sent to Kafka
/// </summary>
/// <typeparam name="TKey">Type of the record key</typeparam>
/// <typeparam name="TValue">Type of the record value</typeparam>
public class ProducerRecord<TKey, TValue>
{
    /// <summary>
    /// The topic to send the record to
    /// </summary>
    public string Topic { get; }

    /// <summary>
    /// The partition to send the record to (optional)
    /// </summary>
    public int? Partition { get; }

    /// <summary>
    /// The timestamp for the record (optional)
    /// </summary>
    public long? Timestamp { get; }

    /// <summary>
    /// The key for the record (optional)
    /// </summary>
    public TKey? Key { get; }

    /// <summary>
    /// The value for the record
    /// </summary>
    public TValue? Value { get; }

    /// <summary>
    /// Headers for the record (optional)
    /// </summary>
    public IDictionary<string, byte[]>? Headers { get; }

    public ProducerRecord(string topic, TValue? value)
        : this(topic, null, null, default(TKey), value, null)
    {
    }

    public ProducerRecord(string topic, TKey? key, TValue? value)
        : this(topic, null, null, key, value, null)
    {
    }

    public ProducerRecord(string topic, int? partition, TKey? key, TValue? value)
        : this(topic, partition, null, key, value, null)
    {
    }

    public ProducerRecord(string topic, int? partition, long? timestamp, TKey? key, TValue? value, IDictionary<string, byte[]>? headers)
    {
        Topic = topic ?? throw new ArgumentNullException(nameof(topic));
        Partition = partition;
        Timestamp = timestamp;
        Key = key;
        Value = value;
        Headers = headers;
    }

    public override string ToString()
    {
        return $"ProducerRecord(topic={Topic}, partition={Partition}, headers={Headers}, key={Key}, value={Value}, timestamp={Timestamp})";
    }
}

/// <summary>
/// Metadata for a record that was sent to Kafka
/// </summary>
public class RecordMetadata
{
    /// <summary>
    /// The topic the record was sent to
    /// </summary>
    public string Topic { get; }

    /// <summary>
    /// The partition the record was sent to
    /// </summary>
    public int Partition { get; }

    /// <summary>
    /// The offset of the record in the partition
    /// </summary>
    public long Offset { get; }

    /// <summary>
    /// The timestamp of the record
    /// </summary>
    public long Timestamp { get; }

    /// <summary>
    /// The serialized size of the key
    /// </summary>
    public int SerializedKeySize { get; }

    /// <summary>
    /// The serialized size of the value
    /// </summary>
    public int SerializedValueSize { get; }

    public RecordMetadata(string topic, int partition, long offset, long timestamp, 
                         int serializedKeySize, int serializedValueSize)
    {
        Topic = topic;
        Partition = partition;
        Offset = offset;
        Timestamp = timestamp;
        SerializedKeySize = serializedKeySize;
        SerializedValueSize = serializedValueSize;
    }

    public override string ToString()
    {
        return $"{Topic}-{Partition}@{Offset}";
    }
}