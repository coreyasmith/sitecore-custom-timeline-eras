# 💹 Custom Timeline Eras

This project demonstrates how to trigger outcomes and display custom outcomes as
eras on the Experience Profile timeline in Sitecore.

## 🚀 Usage

1. Install a new instance of [Sitecore 9.3][1].
2. Update the `publishUrl` in [`PublishSettings.Sitecore.targets`][2] to your
   Sitecore installation's web root (e.g., `C:\inetpub\wwwroot\sc93.sc`).
3. Update the `publishUrl` in
   [`PublishSettings.xConnect.IndexWorker.targets`][3] to your xConnect index
   worker's install folder (e.g.,
   `C:\inetpub\wwwroot\sc93.xconnect\App_Data\jobs\continuous\IndexWorker`).
4. Update the `physicalRootPath` in [`CustomSerializationFolder.config`][4] to
   point to the root of this repository on your disk.
5. Build and publish both projects in the solution.
6. Publish the site from Sitecore.
7. Open the home page in an incognito window or clear your cookies.
8. Click the `Identify Contact` button to identify the current session.
9. Trigger some outcomes.
10. Click the `Abandon Session` button to force the outcomes to show up on the
    Experience Profile.
11. Open the Experience Profile from the Sitecore Launchpad and click
    *Bruce Wayne* to see the outcomes on the timeline.
12. Enjoy.

## 💡 Notes

This repository contains Sitecore Support patch
[`Sitecore.Support.126998.134727`][5] to fix an issue with the timeline. Please
see the [`README.md` with that patch][6] for more information.

[1]: https://dev.sitecore.net/Downloads/Sitecore_Experience_Platform/93/Sitecore_Experience_Platform_93_Initial_Release.aspx
[2]: PublishSettings.Sitecore.targets
[3]: PublishSettings.xConnect.IndexWorker.targets
[4]: src/CustomTimelineEras/App_Config/Include/z.CustomTimelineEras.Serialization/CustomSerializationFolder.config
[5]: https://github.com/SitecoreSupport/Sitecore.Support.126998.134727
[6]: src/CustomTimelineEras/sitecore/shell/client/Business%20Component%20Library/Layouts/Renderings/Common/Timelines/README.md
