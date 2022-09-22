

from ftplib import MAXLINE
from re import S, T
from sre_compile import MAXCODE
from subprocess import HIGH_PRIORITY_CLASS
from traceback import print_tb
from turtle import pos, position, st
from xml.etree.ElementTree import ProcessingInstruction
from numpy import array, column_stack, mat, matrix
import numpy as np
from pyparsing import col, srange



class Solution(object):

    def maximalRectangle(self, matrix):
        """
        :type matrix: List[List[str]]
        :rtype: int
        """
        if not matrix or not matrix[0]: return 0
        M, N = len(matrix), len(matrix[0])
        
        height = [0] * N
        column = [0] * N
        
        res = 0
        rownumber = 0
        max_row_number = 0
        maxrow = []
        max_column_number = 0
        maxcolumn = []
        
        
        for row in matrix:
            #print(row)
            for i in range(N):

                if row[i] == '0': #如果列向量內，i位元為0
                    

                    height[i] = 0 #height的i位元就為0
                    column[i] = 0

                else:

                    height[i] += 1 #如果height的i位元為1則加1
                    column[i] += 1

            rownumber = rownumber + 1
            if self.maxRectangleArea(height, N)[0] > res:               
                maxcolumn = column * 1 # 因為column是list，沒加str或其他運算，回傳maxcolum會變成column的最終結果
                max_row_number = rownumber
                maxrow = row
                MaxW = self.maxRectangleArea(height, N)[1]
                MaxH = self.maxRectangleArea(height, N)[2]
                res = max(res, self.maxRectangleArea(height, N)[0])
                max_column_number = self.maxLength(maxrow, maxcolumn, MaxW, MaxH, res)
                max_row2height = height * 1
                
        return res, max_row_number, max_column_number, MaxH, MaxW, max_row2height
    
                    
            
    def maxRectangleArea(self, height, N):
        if not height: return 0
        res = 0
        MaxH = 0
        MaxW = 0
        stack = list()
        if len(height) <= N:
            height.append(0) #height從上面定義過來，一過來就被append一個0，height向量是matrix的各個row，所以一開始就有五位，由五位變六位以此類推
        for i in range(len(height)):#
#            print("height的長度 = ",len(height), "長度-1為i的最大值")
#            print("next i = ",i)
            cur = height[i]
#            print("cur = height[i] = ", height[i])
#            print("stack up = ", stack, ",位置0、1、2、...以此類推")
            #當stack 不等於 [] 且cur(height[i]) < height[最後一位]、height[倒數第二位]、...以此類推;
            #進while迴圈的條件就是row中當前數字比上一個數字小，cur是當前數字，height[stack[-1]]是"i"被存在stack list中的位置，
            #因為符合while條件進入迴圈，
            while stack != [] and (cur < height[stack[-1]]): 
#                print("i = ",i)
#                print("stack[-1] = ",stack[-1]) #stack[-1]是stack list中最後一個位置的數字
#                print("height = ",height)
                #pop在這裡是:回傳stack list中，最後一個數字的值，並且把最後一個數字移除，用來選height中從位置"0"開始算的數字;stack list是從for迴圈中的"i"來的，
                #所以選最後一個值就是在height中的 "i"位置; 
#                print("over pop's stack = ", stack)
                w = height[stack.pop()] 
#                print("under pop's stack = ", stack)
                #print(w)
                #h = i if not stack else i - stack[-1] - 1 #如果stack為ture，h = i，不是的話h = i - stack[-1] -1

                #如果stack為ture，h = i，不是的話h = i - stack[-1] -1，要注意stack[-1]已經在最後一位，while會向左移stack list;
                #[1,3,4]在if外已經在4的位置，進來後要再往前一位變成3
                if stack != []: 
#                    print("i - stack[-1] = ", i - stack[-1])
#                    print("stack[-1] = ", stack[-1])
                    #stack也會記錄row連續的"1"，因為進入while的條件是height中當前數字小於前一位數字，有連續的話數字會相同，一直找到比較小的才會停止並開始算記錄到的 "i"位置;
                    #其實也只有當前數字及前一位數字相乘會是最大的
                    #第三row是連續5個1，但是stack在紀錄"i"的時候，只要有進while就會
                    h = i - stack[-1] -1
                else: 
                    h = i #這裡只用在row從第一位開始連續為1的時候，如果中斷連續，都用上面if的 h
                    
#                print("w,h = ",w,h)
#                print(height)
                if w * h > res:
                    res = w * h
#                    print("currentheigh = ", height)
                    MaxW = w
                    MaxH = h
                    
                #res = max(res, w * h) 
            

            stack.append(i) #跟while同層，進去while的stack list會變成 []，沒進while就新增現在的"i"到下一個位置，如:[0]、[1,3]、[1,3,4]...等，只要離開for就會重置stack list
#            print("stack down = ",stack)
        #print("height_B = ", height)
        return res, MaxW, MaxH, height     
        
        


    # max_column_number
    def maxLength(self, maxrow, maxcolumn, MaxW, MaxH, res):
        position = 0 
        store_column_position = 0
        column_length = 0
        for i in range(len(maxcolumn)):
            position = position + 1
            if maxcolumn[i] >= MaxW:
                column_length = column_length + 1
            else :
                column_length = 0

            # 先找出maxcolumn中大於等於MaxW的值，並且比前一個值還小
          
            if (column_length * MaxW == res) or (MaxH == 1 and maxcolumn[i] != 0)\
                and store_column_position == 0:
                # print(res)
                store_column_position = position
                column_length = 0
                # print("res, = ", res, store_column_position)
        return store_column_position

                







"""
if __name__ == '__main__':
    '''t
    儲存矩陣
        [
        ["1", "1"],
        ["1", "0"]
        ]

        [
        ["1","0","1","0","0"],
        ["1","0","1","1","1"],
        ["1","1","1","1","1"],
        ["1","0","0","1","0"]
        ]

        [
        ["1","0","1","0","0","0"],
        ["1","0","1","1","1","0"],
        ["1","1","1","1","1","0"],
        ["1","1","1","1","1","0"],
        ["1","0","1","1","1","1"]
        ]

        [
        ["1","0","1","0","0","0","0","0","0"],
        ["1","0","1","1","0","0","0","0","0"],
        ["1","0","1","1","1","0","1","1","0"],
        ["1","0","1","1","1","0","1","1","0"],
        ["1","1","0","1","1","0","0","0","1"]
        ]

        [
        ["1","0","1","0","0","0","0","0","0"],
        ["1","0","1","1","0","0","0","1","0"],
        ["1","0","1","1","0","0","1","1","0"],
        ["1","0","1","1","1","0","1","1","0"],
        ["1","1","0","1","1","0","0","0","1"]
        ]
    '''
    
    # 讀txt內的矩陣 def內的資料格式是 1.type matrix: List[List[str]]、2.rtype: int
    
    matrix=[]
    f = open('pixel_position_test.txt','r')
    for line in f:
        matrix.append(list(line.strip('\n').split(',')))
    f.close()
    


    
    
    maxrectangle = Solution()
    
    #矩陣型式：matrix2 = matrix1.maximalRectangle([["1", "0", "1", "0", "0"],["1", "0", "1", "1", "1"], ["1", "1", "1", "1", "1"], ["1", "0", "0", "1", "0"]])
    find_max_rectangle = maxrectangle.maximalRectangle(matrix)
    
    print("max_area =          ", find_max_rectangle[0])
    print("max_row_number =    ", find_max_rectangle[1])
    print("max_column_number = ", find_max_rectangle[2])
    print("MaxH =              ", find_max_rectangle[3])
    print("MaxW =              ", find_max_rectangle[4])



"""