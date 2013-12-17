import("library.avs")
x = "chunks\chunk005.avi"
video = loadFFmpeg(x)
return FadeInTime(video, 200)
