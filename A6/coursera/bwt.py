# python3
import sys

def BWT(text):
    matrix=[]
    
    l=len(text)
    i=0
    while i<l:
        new_text=text[l-1]
        new_text+=text[0:len(text)-1]
        text=new_text
        matrix.append(text)
        i+=1
    # for i in range(len(matrix)):
    #     for j in range(i+1,len(matrix)):
    #         for k in range(len(matrix[i])):
    #             if matrix[i][k]>matrix[j][k]:
    #                 matrix[i],matrix[j]=matrix[j],matrix[i]
    #                 break
    #             elif matrix[i][k]==matrix[j][k]:
    #                 continue
    #             else:
    #                 break
    matrix.sort()

    last=""
    for i in range(len(matrix)):
        last+=matrix[i][-1]
    return last

if __name__ == '__main__':
    text = sys.stdin.readline().strip()
    print(BWT(text))