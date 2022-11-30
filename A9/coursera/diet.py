# python3
from sys import stdin
import copy


class Equation:
    def __init__(self, a, b):
        self.a = a
        self.b = b

class Position:
    def __init__(self, column, row):
        self.column = column
        self.row = row

def ReadEquation():
    size = int(input())
    a = []
    b = []
    for row in range(size):
        line = list(map(float, input().split()))
        a.append(line)
        b.append(line[size])
    return Equation(a, b)
    

    # size = int(input())
    # a = []
    # # b = []
    # for row in range(size):
    #     line = list(map(float, input().split()))
    #     a.append(line)
    #     # b.append(line[size])
    # # return Equation(a, b)
    # return size,a

def SelectPivotElement(pivot_element,a, used_rows, used_columns):
    # This algorithm selects the first free element.
    # You'll need to improve it to pass the problem.
    while used_rows[pivot_element.row] or a[pivot_element.row][pivot_element.column] == 0:
        pivot_element.row += 1
    if pivot_element.row == len(a):
        return False
    else:
        return pivot_element
    # pivot_element = Position(0, 0)
    # while used_rows[pivot_element.row]:
    #     pivot_element.row += 1
    # while used_columns[pivot_element.column]:
    #     pivot_element.column += 1
    # return pivot_element

def SwapLines(a, b, used_rows, pivot_element):
    a[pivot_element.column], a[pivot_element.row] = a[pivot_element.row], a[pivot_element.column]
    b[pivot_element.column], b[pivot_element.row] = b[pivot_element.row], b[pivot_element.column]
    used_rows[pivot_element.column], used_rows[pivot_element.row] = used_rows[pivot_element.row], used_rows[pivot_element.column]
    pivot_element.row = pivot_element.column

def ProcessPivotElement(a, b, pivot_element,used):
    # Write your code here
    k=a[pivot_element.row][pivot_element.column]
    if k!=1:
        for i in range(len(a[pivot_element.row])):
            a[pivot_element.row][i]/=k
        b[pivot_element.row]/=k
    for i in range(len(a)):
        if i==pivot_element.row or a[i][pivot_element.column]==0:
            continue
        k=a[i][pivot_element.column]/a[pivot_element.row][pivot_element.column]
        for j in range(len(a[i])):
            a[i][j]-=k*a[pivot_element.row][j]
        b[i]-=k*b[pivot_element.row]
    used[pivot_element.row] = True
        


def MarkPivotElementUsed(pivot_element, used_rows, used_columns):
    used_rows[pivot_element.row] = True
    used_columns[pivot_element.column] = True

def SolveEquation(equation):
    a = equation.a
    b = equation.b
    size = len(a)

    used_columns = [False] * size
    used_rows = [False] * size
    for step in range(size):
        pivot_element = Position(step, 0)
        pivot_element = SelectPivotElement(pivot_element,a, used_rows, used_columns)
        if pivot_element:
            SwapLines(a, b, used_rows, pivot_element)
            ProcessPivotElement(a, b, pivot_element,used_rows)
        else:
            return None
        # MarkPivotElementUsed(pivot_element, used_rows, used_columns)
    return b


def findingSubsets(allEquetions,N,M,pivot,subsets,pivotToAdd):
    pivot.append(pivotToAdd)
    if(len(pivot)!=M):
        for i in range(pivotToAdd+1,N+M+1):
            if i not in pivot:
                copy= pivot[:]
                findingSubsets(allEquetions,N,M,pivot,subsets,i)
                pivot=copy
    else:
        subsets.append(pivot)

def checking( N, M,solution, A,b):
    for i in range(M):
        if(solution[i]<0):
            return False
    for i in range(N):
        sum=0
        for j in range(M):
            sum+=solution[j]*A[i][j]
        if(sum-b[i]>0.000000001):
            return False
    return True

def solve_diet_problem(N, M, A, b, c):  
  # Write your code here
    allEquetions=[]
            
    for i in range(N+M+1):
        allEquetions.append(i)
    subsets=[]
    for i in range(N+2):
        findingSubsets(allEquetions,N,M,[],subsets,i)
    
    
    for i in range(M):
        row =[]
        for j in range(M):
            if(i==j):
                row.append(-1)
            else:
                row.append(0)
        b.append(0)
        A.append(row)
    A.append([1]*M)
    b.append(100000000000)


    solutions=[]
    for i in range(len(subsets)):
        new_A=[]
        new_b=[]
        for count in range(M):
            new_A_l=[]
            for j in range(M):
                new_A_l.append(A[subsets[i][count]][j])
            new_b.append(b[subsets[i][count]])
                # newMatrix[count,j]=b[subsets[i][count]][j]
            new_A.append(new_A_l)
        try:
            solutions.append(SolveEquation(Equation(new_A,new_b)))
        except:
            continue
    max=-99999999
    maxIndex=-1
    for i in range(len(solutions)):
        if(checking(N,M,solutions[i],A,b)):
            sum=0
            for j in range(M):
                sum+=solutions[i][j]*c[j]
            if(sum>=max):
                max=sum
                maxIndex=i
    if(maxIndex==-1):
        return "No solution"
    
    # for i in range(len(solutions[maxIndex])):
    #     ans=solutions[maxIndex][i]
        # double ansReal=(int) ans;
        # double ansAshar=ans%1;
        # if(ansAshar>0.25 && ansAshar<0.75)
        # {
        #     ansReal+=0.5;
        # }
        # else if(ansAshar>0.75)
        # {
        #     ansReal+=1;
        # }
        # solutions[maxIndex][i]=ansReal;
    else:
        temp=0
        for s in solutions[maxIndex]:
            temp += s
        if temp > 1000000000:
            return "Infinity"
        else:
            str="Bounded solution\n"
            str+=" ".join(map(lambda x : '%.18f' % x,solutions[maxIndex]) )
            return str



# n, m = list(map(int, stdin.readline().split()))
# A = []
# for i in range(n):
#   A += [list(map(int, stdin.readline().split()))]
# b = list(map(int, stdin.readline().split()))
# c = list(map(int, stdin.readline().split()))

n, m = list(map(int, input().split()))
A = []
for i in range(n):
    A += [list(map(int, input().split()))]
b = list(map(int, input().split()))
c = list(map(int, input().split()))

# anst, ansx = solve_diet_problem(n, m, A, b, c)
res=solve_diet_problem(n,m,A,b,c)
print(res)


# if anst == -1:
#   print("No solution")
# if anst == 0:  
#   print("Bounded solution")
#   print(' '.join(list(map(lambda x : '%.18f' % x, ansx))))
# if anst == 1:
#   print("Infinity")
    
