# 💹 Custom Timeline Eras

This project demonstrates how to trigger outcomes and display custom outcomes as
eras on the Experience Profile timeline in Sitecore.

## 💼 Prerequisites

Since this functionality depends on xDB, MongoDB must be installed and running
for this solution to work.

## 🚀 Usage

1. Install a new instance of [Sitecore 8.2 Update-7][1].
2. Update the `publishUrl` in [`PublishSettings.targets`][2] to point to your
   Sitecore installation's `Website` folder.
3. Update the `physicalRootPath` in [`CustomSerializationFolder.config`][3] to
   point to the root of this repository on your disk.
4. Build and publish the solution.
5. Publish the site from Sitecore.
6. Open the home page in an incognito window or clear your cookies.
7. Click the `Identify Contact` button to identify the current session.
8. Trigger some outcomes.
9. Click the `Abandon Session` button to force the outcomes to show up on the
   Experience Profile.
10. Open the Experience Profile from the Sitecore Launchpad and click
    *Bruce Wayne* to see the outcomes on the timeline.
11. Enjoy.

## 💡 Notes

This repository contains Sitecore Support patch
[`Sitecore.Support.126998.134727`][4] to fix an issue with the timeline. Please
see the [`README.md` with that patch][5] for more information.

[1]: https://dev.sitecore.net/Downloads/Sitecore_Experience_Platform/82/Sitecore_Experience_Platform_82_Update7.aspx
[2]: PublishSettings.targets
[3]: src/CustomTimelineEras/App_Config/Include/z.CustomTimelineEras.Serialization/CustomSerializationFolder.config
[4]: https://github.com/SitecoreSupport/Sitecore.Support.126998.134727
[5]: src/CustomTimelineEras/sitecore/shell/client/Business%20Component%20Library/Layouts/Renderings/Common/Timelines/README.md
