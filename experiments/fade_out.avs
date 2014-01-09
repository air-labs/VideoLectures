import("library.avs")
x = "chunks\chunk005.avi"
video = DirectShowSource(x)
return FadeOutTime(video, 200)
