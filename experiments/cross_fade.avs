import("library.avs")
a = DirectShowSource("chunks\chunk009.avi")
b = DirectShowSource("chunks\chunk006.avi")
return CrossFadeTime(a, b, 200)
