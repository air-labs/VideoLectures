PATH C:\ffmpeg\bin\
del *.mp4

ffmpeg -loop 1 -f image2 -i 01.png -c:v libx264 -t 1 01.mp4
ffmpeg -loop 1 -f image2 -i 02.png -c:v libx264 -t 1 02.mp4
ffmpeg -loop 1 -f image2 -i 03.png -c:v libx264 -t 1 03.mp4
ffmpeg -f concat -i ConcatFilesList.txt -c copy 123.mp4
ffmpeg -i 123.mp4 -ss 0.5 -t 2 1223.mp4
ffmpeg -i sample.avi -vn -ac 2 -ar 44100 -ab 320k -f mp3 sound.mp3
ffmpeg -i 1223.mp4 -i sound.mp3 -vcodec copy -acodec copy 1223Loud.mp4