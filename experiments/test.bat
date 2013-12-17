PATH C:\ffmpeg\bin\
del *.mp4

ffmpeg -i fade_in.avs		fade_in.mp4 -vb 100 
ffmpeg -i fade_out.avs		fade_out.mp4 -vb 100 
ffmpeg -i cross_fade.avs	cross_fade.mp4 -vb 100 
ffmpeg -i intro.avs			intro.mp4 -vb 100 
ffmpeg -i watermark.avs		watermark.mp4 -vb 100 
