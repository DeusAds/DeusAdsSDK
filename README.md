# DeusAdsTool

DeusAdsTool is a Unity package designed to integrate an ad service into your Unity projects. This package allows you to display advertisements on billboards and other surfaces in your game. The MVP version provides functionality to fetch and display ad images from a URL.

## Features

- **Billboard Ad Display:** Attach the `BillboardAd` script to a game object to display advertisements.
- **Ad Service Integration:** Use the `AdService` script to handle ad image URLs and API key validation.
- **Customizable Ads:** Set the ad image URL directly or use a placeholder image.

## Installation

To install the DeusAdsTool package, follow these steps:

1. **Add the Package:**
   - Open your Unity project.
   - Navigate to `Window` > `Package Manager`.
   - Click the “+” button and select “Add package from Git URL”.
   - Enter the URL of the DeusAdsTool repository and click “Add”.

   Alternatively, you can add the package to your `manifest.json` file:

   ```json
   "com.DeusAds.deusadstool": "https://github.com/DeusAds/DeusAdsTool.git"


Usage
Setup
Attach AdService Script:

Create an empty GameObject in your scene (e.g., AdManager).
Attach the AdService script to this GameObject.
Set the apiKey field in the AdService component to the API key provided to you.
Attach BillboardAd Script:

Create a 3D object to act as a billboard (e.g., a Plane).
Attach the BillboardAd script to the billboard object.
Set the imageUrl field in the BillboardAd component to the URL of the ad image or leave it blank to use a placeholder image.
Example
Here’s an example of how to set up a billboard in your scene:

Create a Billboard:

Right-click in the Hierarchy window, select 3D Object > Plane.
Rename the plane to Billboard.
Configure the Billboard:

Select the Billboard object in the Hierarchy.
In the Inspector, click Add Component and add the BillboardAd script.
Set the imageUrl property to a valid image URL or leave it blank.
Configure AdService:

Create an empty GameObject in the scene (e.g., AdManager).
Attach the AdService script to this GameObject.
Set the apiKey field to your provided API key.
Run the Scene:

Press Play in the Unity Editor.
The billboard should display the ad image specified by the URL.
License
This package is licensed under the MIT License. See the LICENSE file for details.

Contributing
Contributions are welcome! Please fork the repository and submit a pull request with your changes.

Contact
For questions or support, please contact your.email@company.com.

Changelog
[1.0.0] - YYYY-MM-DD
Initial release with basic functionality to display ads on billboards.


