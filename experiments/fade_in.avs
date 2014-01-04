import("library.avs")
x = "chunks\chunk005.avi"
video = DirectShowSource(x)
return FadeInTime(video, 200)
