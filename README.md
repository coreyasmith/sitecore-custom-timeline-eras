# Custom Timeline Era

This project demonstrates how to trigger outcomes and display custom outcomes as
eras on the Experience Profile Timeline in Sitecore.

## Usage

1. Install a new instance of [Sitecore 8.2 Update-4][1].
2. Update the `publishUrl` in [`PublishSettings.targets`][2] to point to your
   Sitecore installation's `Website` folder.
3. Update the `physicalRootPath` in [`CustomSerializationFolder.config`][3] to
   point to the root of this repository on your disk.
4. Build and publish the solution.
5. Publish the site from Sitecore.
6. Open the home page and trigger some outcomes.
7. Click the `Abandon Session` button to force the outcomes to show up on the
   Experience Profile.
8. Open the Experience Profile from the Sitecore Launchpad and click
   *Bruce Wayne* to see the outcomes on the timeline.
9. Enjoy.

[1]: https://dev.sitecore.net/Downloads/Sitecore_Experience_Platform/82/Sitecore_Experience_Platform_82_Update4.aspx
[2]: PublishSettings.targets
[3]: CustomTimelineEra/App_Config/Include/z.CustomTimelineEra.Serialization/CustomSerializationFolder.config
