# python3
V, E = map(int, input().split())
matrix = [ list(map(int, input().split())) for i in range(E) ]

def makeAdj(V,E,matrix):
    result =[]
    for i in range(V+1):
        result.append({})
    for i in range(E):
        result[matrix[i][0]][matrix[i][1]]=True
        result[matrix[i][1]][matrix[i][0]]=True
    return result

# This solution prints a simple satisfiable formula
# and passes about half of the tests.
# Change this function to solve the problem.
# def printEquisatisfiableSatFormula():
    # print("3 2")
    # print("1 2 0")
    # print("-1 -2 0")
    # print("1 -2 0")
onlyOne=[]
Adj=makeAdj(V,E,matrix)
for i in range(1,V+1):
    onlyoneOR=[0 for j in range(V)]
    onlyonePathOR=[0 for j in range(V)]
    for j in range(1,V+1):
        onlyoneOR[j-1]=str(i*V+j)
        onlyonePathOR[j-1]=str(j*V+i)
        for k in range(j+1,V+1):
            onlyOne.append([str(-(i*V+j)),str(-(i*V+k))])
            onlyOne.append([str(-(j*V+i)),str(-(k*V+i))])
        if i!=j and not j in Adj[i]:
            for k in range(1,V):
                onlyOne.append([str(-(i*V+k)),str(-(j*V+(k+1)))])
    onlyOne.append(onlyoneOR)

# printEquisatisfiableSatFormula()
if len(onlyOne):
    print(str(len(onlyOne)),end=' ')
    print(str(V*V*2))
    for i in range(len(onlyOne)):
        # List<string> newstr=new List<string>();
        for j in range(len(onlyOne[i])):
            print(onlyOne[i][j],end=' ')
        print("0")
else:
    print("1 1")
    print("1 -1 0")