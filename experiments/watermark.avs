import("library.avs")
video = DirectShowSource("chunks\chunk009.avi")
return AddWatermarkPNG(video, "rabbids.png", video.width/2-120, video.height-250)