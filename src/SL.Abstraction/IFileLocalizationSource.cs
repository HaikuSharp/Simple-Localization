using System.Threading.Tasks;

namespace SL.Abstraction;

/// <summary>
/// Represents a localization source that reads from and writes to a file.
/// </summary>
public interface IFileLocalizationSource : ILocalizationSource
{
    /// <summary>
    /// Gets the file path of the localization source.
    /// </summary>
    string FilePath { get; }

    /// <summary>
    /// Loads localization data from the file.
    /// </summary>
    void Load();

    /// <summary>
    /// Saves localization data to the file.
    /// </summary>
    void Save();

    /// <summary>
    /// Asynchronously loads localization data from the file.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task LoadAsync();

    /// <summary>
    /// Asynchronously saves localization data to the file.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SaveAsync();
}