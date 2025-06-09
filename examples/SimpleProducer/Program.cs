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

using Kafka.Clients.Producer;

namespace SimpleProducer;

/// <summary>
/// Simple example demonstrating the converted Kafka .NET API
/// Note: This is just a demonstration of the interface - the actual implementation is not included
/// </summary>
public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Apache Kafka .NET Port - Simple Producer Example");
        Console.WriteLine("=================================================");
        Console.WriteLine();
        
        // This demonstrates how the API would be used
        // Note: Implementation is not included in this proof-of-concept
        
        Console.WriteLine("Creating producer record...");
        var record = new ProducerRecord<string, string>(
            topic: "my-topic",
            key: "example-key", 
            value: "Hello from Kafka .NET!"
        );
        
        Console.WriteLine($"Record created: {record}");
        Console.WriteLine();
        
        // Example of what the producer API would look like
        Console.WriteLine("Producer API Example:");
        Console.WriteLine("// Configuration would be provided here");
        Console.WriteLine("// using var producer = new KafkaProducer<string, string>(config);");
        Console.WriteLine("// var metadata = await producer.SendAsync(record);");
        Console.WriteLine("// Console.WriteLine($\"Sent to {metadata.Topic}-{metadata.Partition}@{metadata.Offset}\");");
        Console.WriteLine();
        
        // Demonstrate record metadata
        var metadata = new RecordMetadata(
            topic: "my-topic",
            partition: 0,
            offset: 12345,
            timestamp: DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            serializedKeySize: 11,
            serializedValueSize: 22
        );
        
        Console.WriteLine($"Example metadata: {metadata}");
        Console.WriteLine();
        
        Console.WriteLine("This proof-of-concept demonstrates:");
        Console.WriteLine("✓ Converted Java classes to idiomatic C#");
        Console.WriteLine("✓ Maintained API compatibility concepts");  
        Console.WriteLine("✓ Used modern C# features (nullable reference types, async/await)");
        Console.WriteLine("✓ Followed .NET naming conventions");
        Console.WriteLine("✓ Created proper project structure");
        Console.WriteLine();
        Console.WriteLine("For a complete implementation, see README-CSHARP-PORT.md");
    }
}