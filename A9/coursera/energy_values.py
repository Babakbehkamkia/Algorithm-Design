# python3

# from _typeshed import AnyPath


EPS = 1e-6
PRECISION = 20

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

def SolveEquation2(equation):
    a = equation.a
    b = equation.b
    size = len(a)

    used_columns = [False] * size
    used_rows = [False] * size
    for step in range(size):
        pivot_element = Position(step, 0)
        pivot_element = SelectPivotElement(pivot_element,a, used_rows, used_columns)
        SwapLines(a, b, used_rows, pivot_element)
        ProcessPivotElement(a, b, pivot_element,used_rows)
        # MarkPivotElementUsed(pivot_element, used_rows, used_columns)
    return b

def SolveEquation(MATRIX_SIZE,matrix):
    row=0 
    col=0
    while (row<MATRIX_SIZE):
        value_to_divide=matrix[row][col]
        if (value_to_divide!=0):
            if (value_to_divide!=1):
                for i in range(MATRIX_SIZE+1):
                    matrix[row][i]/=value_to_divide
            for i in range(MATRIX_SIZE):
                if (i==row):
                    continue
                k=matrix[i][col]/matrix[row][col]
                for j in range(MATRIX_SIZE+1):
                    matrix[i][j]-=k*matrix[row][j]
            row+=1
            col+=1
        else:
            row_to_change=row+1
            while (row_to_change<MATRIX_SIZE):
                if (matrix[row_to_change,col]!=0):
                    matrix[row, i] , matrix[row_to_change, i]=matrix[row_to_change, i],matrix[row, i]
                    # for i in range(MATRIX_SIZE):
                    #     t = matrix[row, i]
                    #     matrix[row, i] = matrix[row_to_change, i]
                    #     matrix[row_to_change, i] = t
                    break
                row_to_change+=1
            # if (row_to_change==MATRIX_SIZE):?
            #     return new double[MATRIX_SIZE]

    result =[]
    for j in range(MATRIX_SIZE):
        for i in range(MATRIX_SIZE):
            if(matrix[i][j]==1):
                ans=matrix[i][MATRIX_SIZE]
                # ansReal=int(ans)
                # ansAshar=ans%1
                # if(ansReal<0):
                #     if(ansAshar<-0.25 and ansAshar>-0.75):
                #         ansReal-=0.5
                #     elif(ansAshar<-0.75):
                #         ansReal-=1

                # else:
                #     if(ansAshar>0.25 and ansAshar<0.75):
                #         ansReal+=0.5
                #     elif(ansAshar>0.75):
                #         ansReal+=1
                # result.append(ansReal)
                result.append(ans)
                break
    return result

def PrintColumn(column):
    size = len(column)
    for row in range(size):
        print("%.6f" % column[row])

if __name__ == "__main__":
    # size,equation = ReadEquation()
    # solution = SolveEquation2(size,equation)
    equation = ReadEquation()
    solution = SolveEquation2(equation)
    PrintColumn(solution)
    exit(0)
