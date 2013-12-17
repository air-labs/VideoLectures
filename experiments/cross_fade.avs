import("library.avs")
a = loadFFmpeg("chunks\chunk009.avi")
b = loadFFmpeg("chunks\chunk006.avi")
return CrossFadeTime(a, b, 200)
