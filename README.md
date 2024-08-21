# DeusAdsTool

DeusAdsTool is a Unity package that allows game developers to integrate ad services into their games. This package provides functionality for displaying ads on billboards within your Unity project.

## Installation

To add the DeusAdsTool package to your Unity project, follow these steps:

1. Open Unity and go to `Window` > `Package Manager`.
2. Click on the “+” button and select “Add package from Git URL...”.
3. Enter the following URL: `https://github.com/DeusAds/DeusAdsTool`.
4. Click "Add" to install the package.

## Usage

### Adding Ads to a Billboard

1. **Create a Billboard Object:**
   - In your Unity scene, create a 3D object that will act as a billboard. For example, use a plane:
     - Right-click in the Hierarchy window and choose `3D Object` > `Plane`.
     - Name the object `Billboard`.

2. **Attach the BillboardAd Script:**
   - Select the `Billboard` object in the Hierarchy.
   - In the Inspector window, click on `Add Component`.
   - Search for `BillboardAd` and select it.

3. **Configure the Ad:**
   - In the BillboardAd component, set the `imageUrl` property in the Inspector. You can leave it blank to use a placeholder image or enter a URL to an ad image.

4. **Run Your Scene:**
   - Press `Play` in the Unity Editor to see the ad displayed on the billboard.

### Configuring AdService

1. **Add AdService to a GameObject:**
   - Create an empty GameObject in your scene (e.g., `AdManager`) and attach the `AdService` script to it.

2. **Set API Key:**
   - In the `AdService` component, set the `apiKey` field to the API key provided to you.

3. **Integrate with BillboardAd:**
   - Ensure the `BillboardAd` script retrieves the ad image URL from `AdService` and validates the API key before attempting to load the ad image.

## API Documentation

### `AdService`

- **`public string GetAdImageUrl()`**
  - Returns the URL of the ad image. Requires a valid API key.

- **`public bool IsApiKeyValid()`**
  - Returns whether the API key has been validated.

### `BillboardAd`

- **`public string imageUrl`**
  - URL of the image to display on the billboard. If empty, a placeholder image is used.

## Contribution

If you'd like to contribute to DeusAdsTool, please fork the repository and submit a pull request. For significant changes, please open an issue first to discuss the changes with the maintainers.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.



Changelog
[1.0.0] - YYYY-MM-DD
Initial release with basic functionality to display ads on billboards.


