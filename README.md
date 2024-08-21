# DeusAdsTool

DeusAdsTool is a Unity package that allows game developers to integrate ad services into their games. This package provides functionality for displaying ads on billboards and cars within your Unity project.

## Installation

To add the DeusAdsTool package to your Unity project, follow these steps:

1. Open Unity and go to `Window` > `Package Manager`.
2. Click on the “+” button and select “Add package from Git URL...”.
3. Enter the following URL: `https://github.com/DeusAds/DeusAdsTool.git`.
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
   - In the `BillboardAd` component, set the `imageUrl` property in the Inspector. You can leave it blank to use a placeholder image or enter a URL to an ad image.

4. **Run Your Scene:**
   - Press `Play` in the Unity Editor to see the ad displayed on the billboard.

### Adding Ads to a Car

1. **Prepare the Car Model:**
   - Import your car model into the Unity project.
   - Ensure the car model has a suitable material or surface where the ad will be displayed.

2. **Add an Ad Surface to the Car:**
   - Create a new plane or UI element as a child of the car model, positioned where you want the ad to appear (e.g., the rear window).

3. **Attach the BillboardAd Script to the Ad Surface:**
   - Select the newly created plane or UI element in the Hierarchy.
   - In the Inspector window, click on `Add Component`.
   - Search for `BillboardAd` and select it.

4. **Configure the Ad:**
   - In the `BillboardAd` component, set the `imageUrl` property in the Inspector. You can leave it blank to use a placeholder image or enter a URL to an ad image.

5. **Run Your Scene:**
   - Press `Play` in the Unity Editor to see the ad displayed on the car.

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
  - URL of the image to display on the billboard or car. If empty, a placeholder image is used.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

Changelog
[1.0.0] - YYYY-MM-DD
Initial release with basic functionality to display ads on billboards.


