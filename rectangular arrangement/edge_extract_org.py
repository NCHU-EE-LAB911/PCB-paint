import cv2 as cv
from numpy import imag
from PIL import Image

# 定义图片放缩大小
#img = cv.imread(r'D:\Yao\Pegatron_Unity\extracted.jpeg')
img = Image.open('.\PCB_CoatArea_cls.jpg')
a, b = img.size
#print(b)
# 讀pixel
position_list = str()
count = 0
for i in range(a):
    if count == 0:
        for j in range(b):
            pixel = img.getpixel((i,j))
            if pixel <= ((200,200,200)):
                count = 1
                f = open(r".\EdgeFirstPixel_position-3.txt", "w+")
                f.writelines('far left' + '\n' + str(i) + ' ' + str(j) + '\n')
                break
count = 0
for j2 in range(b):
    if count == 0:
        for i2 in range(a):
            pixel = img.getpixel((i2,j2))
            if pixel <= ((200,200,200)):
                count = 1
                f = open(r".\EdgeFirstPixel_position-3.txt", "a+")
                f.writelines('the top' + '\n' + str(i2) + ' ' + str(j2) + '\n')
                break



