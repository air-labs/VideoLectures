PATH C:\ffmpeg\bin\
del fade*.avi
del cross_fade.avi
del intro.avi
del watermark.avi

ffmpeg -i fade_in.avs -acodec copy -vcodec mpeg4 -r 30 -b:v 13M fade_in.avi
ffmpeg -i fade_out.avs -acodec copy -vcodec mpeg4 -r 30 -b:v 13M fade_out.avi
ffmpeg -i cross_fade.avs -acodec copy -vcodec mpeg4 -r 30 -b:v 13M cross_fade.avi
ffmpeg -i intro.avs -acodec copy -vcodec mpeg4 -r 30 -b:v 13M intro.avi
ffmpeg -i watermark.avs -acodec copy -vcodec mpeg4 -r 30 -b:v 13M watermark.avi
