# python3
from sys import api_version, stdin

eqCount,varCount = list(map(int, stdin.readline().split()))
A = []
for i in range(eqCount):
  A += [list(map(int, stdin.readline().split()))]
b = list(map(int, stdin.readline().split()))

# eqCount,varCount = map(int,input().split())
# A = []
# for i in range(eqCount):
#     l= list(map(int,input().split()))
#     A.append(l)
# b = list(map(int, input().split()))

def findingNonZero(A,m,row):
    nonZero=[]
    for i in range(m):
        if(A[row][i]!=0):
            nonZero.append(i)
    return nonZero
# This solution prints a simple satisfiable formula
# and passes about half of the tests.
# Change this function to solve the problem.
# def printEquisatisfiableSatFormula():
#     print("3 2")
#     print("1 2 0")
#     print("-1 -2 0")
#     print("1 -2 0")

# printEquisatisfiableSatFormula()

result =[]
for i in range(eqCount):
    nonZero=findingNonZero(A, varCount, i)
    if(len(nonZero)==0):
        if(b[i]<0):
            print("2 1")
            print("1 0")
            print("-1 0")

    elif(len(nonZero)==1):
        if(A[i][nonZero[0]]>b[i]):
            result.append([str(-(nonZero[0]+1))])


        if(0>b[i]):
            result.append([str((nonZero[0]+1))])


    elif(len(nonZero)==2):
        if(0>b[i]):
            result.append([str((nonZero[0]+1)),str((nonZero[1]+1))])


        if(A[i][nonZero[0]]>b[i]):
            result.append([str(-(nonZero[0]+1)),str((nonZero[1]+1))])
        if(A[i][nonZero[1]]>b[i]):
            result.append([str((nonZero[0]+1)),str(-(nonZero[1]+1))])


        if(A[i][nonZero[0]]+A[i][nonZero[1]]>b[i]):
            result.append([str(-(nonZero[0]+1)),str(-(nonZero[1]+1))])

    elif(len(nonZero)==3):
        if(0>b[i]):
            result.append([str((nonZero[0]+1)),str((nonZero[1]+1)),str((nonZero[2]+1))])


        if(A[i][nonZero[0]]>b[i]):
            result.append([str(-(nonZero[0]+1)),str((nonZero[1]+1)),str((nonZero[2]+1))])
        if(A[i][nonZero[1]]>b[i]):
            result.append([str((nonZero[0]+1)),str(-(nonZero[1]+1)),str((nonZero[2]+1))])
        if(A[i][nonZero[2]]>b[i]):
            result.append([str((nonZero[0]+1)),str((nonZero[1]+1)),str(-(nonZero[2]+1))])


        if(A[i][nonZero[0]]+A[i][nonZero[1]]>b[i]):
            result.append([str(-(nonZero[0]+1)),str(-(nonZero[1]+1)),str((nonZero[2]+1))])
        if(A[i][nonZero[2]]+A[i][nonZero[1]]>b[i]):
            result.append([str((nonZero[0]+1)),str(-(nonZero[1]+1)),str(-(nonZero[2]+1))])
        if(A[i][nonZero[0]]+A[i][nonZero[2]]>b[i]):
            result.append([str(-(nonZero[0]+1)),str((nonZero[1]+1)),str(-(nonZero[2]+1))])

        if(A[i][nonZero[0]]+A[i][nonZero[1]]+A[i][nonZero[2]]>b[i]):
            result.append([str(-(nonZero[0]+1)),str(-(nonZero[1]+1)),str(-(nonZero[2]+1))])

if len(result):
    print(str(len(result)),end=' ')
    print(str(varCount))
    for i in range(len(result)):
        # List<string> newstr=new List<string>();
        for j in range(len(result[i])):
            print(result[i][j],end=' ')
        print("0")
else:
    print("1 1")
    print("1 -1 0")
# 1 1
# 1 -1 0