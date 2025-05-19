using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BrainsToDo.Helpers;

/// <summary>
/// Represents the full JSON payload:
/// {
///   "items": [ { … } ]
/// }
/// </summary>
public class JobItemsEnvelope
{
    [JsonPropertyName("items")]
    public List<JobItem> Items { get; init; } = [];
}

/// <summary>
/// Represents one element inside "items".
/// </summary>
public class JobItem
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    // “content_text” → ContentText
    [JsonPropertyName("content_text")]
    public string ContentText { get; init; } = string.Empty;

    // “date_modified” → DateModified
    [JsonPropertyName("date_modified")]
    public DateTimeOffset DateModified { get; init; }

    // “_feed_entry” → FeedEntry
    [JsonPropertyName("_feed_entry")]
    public FeedEntry FeedEntry { get; init; } = new();
}

/// <summary>
/// Represents the nested “_feed_entry” object.
/// </summary>
public class FeedEntry
{
    [JsonPropertyName("uuid")]
    public Guid Uuid { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("businessName")]
    public string BusinessName { get; init; } = string.Empty;

    [JsonPropertyName("municipal")]
    public string Municipal { get; init; } = string.Empty;

    [JsonPropertyName("sistEndret")]
    public DateTimeOffset SistEndret { get; init; }
}