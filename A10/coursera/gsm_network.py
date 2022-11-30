# python3
V, E = map(int, input().split())
matrix = [ list(map(int, input().split())) for i in range(E) ]

def makeAdj(V,E,matrix):
    result =[]
    for i in range(V):
        result.append([])
    for i in range(E):
        result[matrix[i][0]-1].Add(matrix[i][1]-1)
        result[matrix[i][1]-1].Add(matrix[i][0]-1)
    return result

# This solution prints a simple satisfiable formula
# and passes about half of the tests.
# Change this function to solve the problem.
# def printEquisatisfiableSatFormula():
#     print("3 2")
#     print("1 2 0")
#     print("-1 -2 0")
#     print("1 -2 0")

# printEquisatisfiableSatFormula()


onlyOne=[]

for currentNode in range(1,V+1):
    arrOfOr=[str((currentNode*3)),str((currentNode*3-1)),str((currentNode*3-2))]
    arrOfAnds1=[str(-(currentNode*3)),str(-(currentNode*3-1))]
    arrOfAnds2=[str(-(currentNode*3-2)),str(-(currentNode*3-1))]
    arrOfAnds3=[str(-(currentNode*3)),str(-(currentNode*3-2))]
    onlyOne.append(arrOfOr)
    onlyOne.append(arrOfAnds1)
    onlyOne.append(arrOfAnds2)
    onlyOne.append(arrOfAnds3)

for i in range(E):
    arr1=[str(-((matrix[i][0])*3)),str(-((matrix[i][1])*3))]
    arr2=[str(-((matrix[i][0])*3-1)),str(-((matrix[i][1])*3-1))]
    arr3=[str(-((matrix[i][0])*3-2)),str(-((matrix[i][1])*3-2))]
    onlyOne.append(arr1)
    onlyOne.append(arr2)
    onlyOne.append(arr3)


# long num=onlyOne.Count;
# string[] ans=new string[num+1];
print(str(4*V+3*E),end=' ')
print(str(3*V))
for i in range(len(onlyOne)):
    # List<string> newstr=new List<string>();
    for j in range(len(onlyOne[i])):
        print(onlyOne[i][j],end=' ')
    print("0")
    # string result=string.Join(" ",newstr);
    # ans[i+1]=result;