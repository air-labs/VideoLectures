PATH C:\ffmpeg\bin\
del *.mp4

ffmpeg -loop 1 -f image2 -i 01.png -c:v libx264 -t 1 01.mp4
ffmpeg -loop 1 -f image2 -i 02.png -c:v libx264 -t 1 02.mp4
ffmpeg -loop 1 -f image2 -i 03.png -c:v libx264 -t 1 03.mp4
ffmpeg -f concat -i ConcatFilesList.txt -c copy 123.mp4
ffmpeg -i 123.mp4 -i en.mp3 -vcodec copy -acodec copy 123-en.mp4
ffmpeg -i 123.mp4 -i de.mp3 -vcodec copy -acodec copy 123-de.mp4
ffmpeg -i 123.mp4 -i fr.mp3 -vcodec copy -acodec copy 123-fr.mp4
