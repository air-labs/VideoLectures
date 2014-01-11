rmdir /s /q chunks
mkdir chunks
cd chunks
ffmpeg -i ..\face-converted.avi -ss 3.564 -t 2.896 -qscale 0 chunk003.avi
ffmpeg -i ..\face-converted.avi -ss 10.83 -t 3.63 -qscale 0 chunk005.avi
ffmpeg -i ..\face-converted.avi -ss 16.666 -t 3.8 -qscale 0 chunk007.avi
ffmpeg -i ..\face-converted.avi -ss 23.294 -t 3.11 -qscale 0 chunk009.avi
ffmpeg -i ..\face-converted.avi -ss 26.305 -t 5.980 -qscale 0 chunk010.avi
ffmpeg -i ..\face-converted.avi -ss 32.285 -t 1.262 -qscale 0 chunk011.avi
cd ..
