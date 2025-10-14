using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SL.Abstraction;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SL.Newtonsoft.JSON;

/// <summary>
/// A file-based localization source that uses JSON format for storage.
/// Supports nested JSON structures with configurable key separators.
/// </summary>
public class JsonFileLocalizationSource(string filePath, string separator, JsonSerializerSettings settings) : IFileLocalizationSource
{
    private readonly string m_Separator = separator;
    private readonly Dictionary<string, RawLocalizedString> m_Raws = [];
    private readonly JsonSerializerSettings m_JsonSettings = settings;

    /// <inheritdoc/>
    public string FilePath => filePath;

    /// <inheritdoc/>
    public IEnumerable<RawLocalizedString> Values => m_Raws.Values;

    /// <inheritdoc/>
    public RawLocalizedString GetRaw(string localizeKey) => m_Raws[localizeKey];

    /// <inheritdoc/>
    public void SetRaw(string localizeKey, string localizeValue) => m_Raws[localizeKey] = new(localizeKey, localizeValue);

    /// <inheritdoc/>
    public void RemoveRaw(string localizeKey) => m_Raws.Remove(localizeKey);

    /// <inheritdoc/>
    public void Clear() => m_Raws.Clear();

    /// <inheritdoc/>
    public void Load()
    {
        if(!File.Exists(FilePath))
        {
            m_Raws.Clear();
            return;
        }

        var json = File.ReadAllText(FilePath);
        var jsonObject = JObject.Parse(json);

        m_Raws.Clear();
        FlattenJsonObject(jsonObject, string.Empty, m_Raws);
    }

    /// <inheritdoc/>
    public void Save()
    {
        var directory = Path.GetDirectoryName(FilePath);
        if(!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var tree = BuildJsonTree(m_Raws.Values);
        var json = JsonConvert.SerializeObject(tree, m_JsonSettings);
        File.WriteAllText(FilePath, json);
    }

    /// <inheritdoc/>
    public async Task LoadAsync()
    {
        if(!File.Exists(filePath)) throw new FileNotFoundException(filePath);

        using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);

        using StreamReader streamReader = new(fileStream);
        using JsonTextReader jsonReader = new(streamReader);

        var source = await JToken.LoadAsync(jsonReader).ConfigureAwait(false);

        FlattenJsonObject(source, string.Empty, m_Raws);
    }

    /// <inheritdoc/>
    public async Task SaveAsync()
    {
        var source = BuildJsonTree(m_Raws.Values);

        string directory = Path.GetDirectoryName(filePath);

        if(!string.IsNullOrEmpty(directory) && !Directory.Exists(directory)) _ = Directory.CreateDirectory(directory);

        using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);

        using StreamWriter streamWriter = new(fileStream);
        using JsonTextWriter jsonWriter = new(streamWriter)
        {
            Formatting = Formatting.Indented
        };

        await source.WriteToAsync(jsonWriter).ConfigureAwait(false);
    }

    private void FlattenJsonObject(JToken token, string currentPath, Dictionary<string, RawLocalizedString> result)
    {
        switch(token.Type)
        {
            case JTokenType.Object:
            foreach(var property in ((JObject)token).Properties())
            {
                var newPath = string.IsNullOrEmpty(currentPath) ? property.Name : currentPath + m_Separator + property.Name;
                FlattenJsonObject(property.Value, newPath, result);
            }
            break;

            case JTokenType.Array:
            var index = 0;
            foreach(var element in (JArray)token)
            {
                var newPath = currentPath + m_Separator + index;
                FlattenJsonObject(element, newPath, result);
                index++;
            }
            break;

            default:
            result[currentPath] = new RawLocalizedString(currentPath, token.ToString() ?? string.Empty);
            break;
        }
    }

    private JObject BuildJsonTree(IEnumerable<RawLocalizedString> flattened)
    {
        var root = new JObject();

        foreach(var raw in flattened)
        {
            var pathParts = raw.Key.Split([m_Separator], System.StringSplitOptions.RemoveEmptyEntries);
            JObject currentObject = root;

            for(int i = 0; i < pathParts.Length - 1; i++)
            {
                var part = pathParts[i];

                if(!currentObject.TryGetValue(part, out var nextToken))
                {
                    nextToken = new JObject();
                    currentObject[part] = nextToken;
                }

                currentObject = (JObject)nextToken;
            }

            currentObject.Add(raw.Key, raw.Value);
        }

        return root;
    }
}