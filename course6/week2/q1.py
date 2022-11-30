#python3

class square:
    def __init__(self,edges):
        self.top=edges[0]
        self.left=edges[1]
        self.down=edges[2]
        self.right=edges[3]

# reading pieces
pieces=[]
for i in range(25):
    m=input()
    m=m[1:-1]

    pieces.append(m.split(','))

# creating the board
result=[]
visiteds=[]
for i in range(5):
    row_to_add=[]
    visited=[]
    for j in range(5):
        row_to_add.append(0)
        visited.append([0 for i in range(len(pieces)-4)])
    result.append(row_to_add)
    visiteds.append(visited)

index_to_delete=[]
for i in range(len(pieces)):
    if (pieces[i][0]=="black" and pieces[i][1]=="black"):
        index_to_delete.append(pieces[i])
        result[0][0]=pieces[i]

    if (pieces[i][0]=="black" and pieces[i][3]=="black"):
        index_to_delete.append(pieces[i])
        result[0][-1]=pieces[i]

    if (pieces[i][2]=="black" and pieces[i][1]=="black"):
        index_to_delete.append(pieces[i])
        result[-1][0]=pieces[i]

    if (pieces[i][2]=="black" and pieces[i][3]=="black"):
        index_to_delete.append(pieces[i])
        result[-1][-1]=pieces[i]

for i in range(len(index_to_delete)):
    pieces.remove(index_to_delete[i])

global_visited=[0 for i in range(len(pieces))]
row=0
col=0
while row <5:
    while col <5:
        if row in [0,4] and col in [0,4]:
            col+=1
            continue
        isFound=False
        if row==0 :
            for i in range(len(pieces)):
                if pieces[i][0]=="black" and visiteds[row][col][i]==0 and global_visited[i]==0 and result[row][col-1][3]==pieces[i][1]:
                    if col==3:
                        if result[row][col+1][1]==pieces[i][3]:
                            isFound=True
                            visiteds[row][col][i]=1
                            global_visited[i]=1
                            result[row][col]=pieces[i]
                            break
                    else:
                        isFound=True
                        visiteds[row][col][i]=1
                        global_visited[i]=1
                        result[row][col]=pieces[i]
                        break
        elif row==2:
            for i in range(len(pieces)):
                if pieces[i][2]=="black" and visiteds[row][col][i]==0 and global_visited[i]==0 and result[row][col-1][3]==pieces[i][1] :
                    if col==3:
                        if result[row][col+1][1]==pieces[i][3]:
                            isFound=True
                            visiteds[row][col][i]=1
                            global_visited[i]=1
                            result[row][col]=pieces[i]
                            break
                    else:
                        isFound=True
                        visiteds[row][col][i]=1
                        global_visited[i]=1
                        result[row][col]=pieces[i]
                        break
        elif col==0:
            for i in range(len(pieces)):
                if pieces[i][1]=="black" and visiteds[row][col][i]==0 and global_visited[i]==0 and result[row-1][col][2]==pieces[i][0]:
                    if row==3:
                        if result[row+1][col][0]==pieces[i][2]:
                            isFound=True
                            visiteds[row][col][i]=1
                            global_visited[i]=1
                            result[row][col]=pieces[i]
                            break
                    else:
                        isFound=True
                        visiteds[row][col][i]=1
                        global_visited[i]=1
                        result[row][col]=pieces[i]
                        break
        elif col==2:
            for i in range(len(pieces)):
                if pieces[i][3]=="black" and visiteds[row][col][i]==0 and global_visited[i]==0 and result[row-1][col][2]==pieces[i][0] and result[row+1][col][0]==pieces[i][2]:
                    if row==3:
                        if result[row+1][col][0]==pieces[i][2]:
                            isFound=True
                            visiteds[row][col][i]=1
                            global_visited[i]=1
                            result[row][col]=pieces[i]
                            break
                    else:
                        isFound=True
                        visiteds[row][col][i]=1
                        global_visited[i]=1
                        result[row][col]=pieces[i]
                        break
        else:
            for i in range(len(pieces)):
                if visiteds[row][col][i]==0 and global_visited[i]==0 and result[row][col-1][3]==pieces[i][1] and result[row-1][col][2]==pieces[i][0] :
                    isFound=True
                    visiteds[row][col][i]=1
                    global_visited[i]=1
                    result[row][col]=pieces[i]
                    break
        if not isFound:
            for i in range(len(pieces)):
                visiteds[row][col][i]=0
            if row==0:
                
                col-=1
                global_visited[pieces.index[result[row][col]]]
                result[row][col]=0
            if row>0 and row<4:
                if col>0 :
                    col-=1
                    global_visited[pieces.index[result[row][col]]]
                    result[row][col]=0
                else:
                    row-=1
                    col=3
                    global_visited[pieces.index[result[row][col]]]
                    result[row][col]=0
            if row==4:
                col-=1
                global_visited[pieces.index[result[row][col]]]
                result[row][col]=0
        col+=1
    col=0
    row+=1


for i in range(5):
    for j in range(4):
        print('(',end='')
        for k in range(3):
            print(result[i][j][k],end=',')
        print(result[i][j][4]+');',end='')
    print('(',end='')
    for k in range(3):
        print(result[i][j][k],end=',')
    print(result[i][j][4]+')')


