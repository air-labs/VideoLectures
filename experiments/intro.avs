import("library.avs")
video = DirectShowSource("chunks\chunk009.avi")
return Intro(video, "tron.jpg", 10000, "Lorem ipsum\ndolor sit amet.", "One two three four five\nsix seven eight nine ten\n1234567890")