# 💹 Custom Timeline Eras

This project demonstrates how to trigger outcomes and display custom outcomes as
eras on the Experience Profile timeline in Sitecore.

## 🏗️ Setup

### 🐳 Docker

1. Build the Sitecore 9.3 docker images using the steps in the
   [Sitecore Docker images repository][1].
2. Build the solution with the `Docker` build configuration.
3. Publish both projects in the solution with the `Docker` publish profiles.
4. On the command line:
   1. `cd C:\[path-to]\sitecore-custom-timeline-eras`
   2. `docker-compose up`

### 💽 Locally

1. Install a new instance of [Sitecore 9.3][2].
2. Update the `publishUrl` in [`PublishSettings.Sitecore.targets`][3] to your
   Sitecore installation's web root (e.g., `C:\inetpub\wwwroot\sc93.sc`).
3. Update the `publishUrl` in
   [`PublishSettings.xConnect.IndexWorker.targets`][4] to your xConnect index
   worker's install folder (e.g.,
   `C:\inetpub\wwwroot\sc93.xconnect\App_Data\jobs\continuous\IndexWorker`).
4. Update the `physicalRootPath` in [`CustomSerializationFolder.config`][5] to
   point to the root of this repository on your disk.
5. Build the solution with the `Debug` build configuration.
6. Publish both projects in the solution with the `Local` publish profile.

## 🚀 Usage

1. Publish the site from Sitecore.
2. Open the home page in an incognito window or clear your cookies.
3. Click the `Identify Contact` button to identify the current session.
4. Trigger some outcomes.
5. Click the `Abandon Session` button to force the outcomes to show up on the
   Experience Profile.
6. Open the Experience Profile from the Sitecore Launchpad and click
   *Bruce Wayne* to see the outcomes on the timeline.
7. Enjoy.

## 💡 Notes
This repository contains Sitecore Support patch
[`Sitecore.Support.126998.134727`][6] to fix an issue with the timeline. Please
see the [`README.md` with that patch][7] for more information.

[1]: https://github.com/sitecore/docker-images
[2]: https://dev.sitecore.net/Downloads/Sitecore_Experience_Platform/93/Sitecore_Experience_Platform_93_Initial_Release.aspx
[3]: PublishSettings.Sitecore.targets
[4]: PublishSettings.xConnect.IndexWorker.targets
[5]: src/CustomTimelineEras/App_Config/Include/z.CustomTimelineEras.Serialization/CustomSerializationFolder.config
[6]: https://github.com/SitecoreSupport/Sitecore.Support.126998.134727
[7]: src/CustomTimelineEras/sitecore/shell/client/Business%20Component%20Library/Layouts/Renderings/Common/Timelines/README.md
