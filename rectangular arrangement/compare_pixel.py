#PCB標準圖形

from numpy import diff


matrix1=[]
f = open('pixel_position_test.txt','r')
for line in f:
    matrix1.append(list(line.strip('\n').split(',')))
f.close()

#轉換後圖形
matrix2=[]
f = open('store_rectangle_matrix.txt','r')
for line in f:
    matrix2.append(list(line.strip('\n').split(',')))
f.close()

#以PCB標準圖形的長寬當標準
M, N = len(matrix1), len(matrix1[0])
print("M,N = ", M, N)
different_pixel = 0
pixel_equal_one = 0
for i in range(M):
    row1 = matrix1[i]
    row2 = matrix2[i]
    for j in range(N):
        if row1[j] != row2[j]:
            different_pixel = different_pixel + 1
            print("x,y = ", i + 1, j + 1)
        if row1[j] == str(1):
            pixel_equal_one = pixel_equal_one + 1
print("pixel_equal_one = ", pixel_equal_one)
print("different_pixel = ", different_pixel)
print("Correct rate = ", (1 - different_pixel / pixel_equal_one) * 100, "%")
