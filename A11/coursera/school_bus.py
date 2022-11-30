# python3
from itertools import permutations
INF = 10 ** 9


def DFS(graph,prev_index,index,depth,start_index,visited,best_ans):
    sum=graph[prev_index][index]
    path=[index]
    if depth==0:
        if graph[start_index][index]!=INF :
            return path,sum+graph[start_index][index]
        return [],INF
    m_sum=INF
    m_path=[]
    for i in range(len(graph[index])):
        if graph[index][i]!=INF and visited[i]==0:
            visited[i]=1
            p,s=DFS(graph,index,i,depth-1,start_index,visited)
            visited[i]=0
            if best_ans<s:
                continue
            if s<=m_sum:
                m_sum=s
                m_path=p
            
    path+=m_path
    sum+=m_sum
    return path,sum


def read_data():
    n, m = map(int, input().split())
    graph = [[INF] * n for _ in range(n)]
    for _ in range(m):
        u, v, weight = map(int, input().split())
        u -= 1
        v -= 1
        graph[u][v] = graph[v][u] = weight
    return graph

def print_answer(path_weight, path):
    print(path_weight)
    if path_weight == -1:
        return
    print(' '.join(map(str, path)))

def optimal_path(graph):
    # This solution tries all the possible sequences of stops.
    # It is too slow to pass the problem.
    # Implement a more efficient algorithm here.
    n = len(graph)
    best_ans = INF
    best_path = []

    for i in range(n):
        depth=n-1
        sum=0
        path=[i]
        index=i
        visited=[0 for k in range(n)]
        visited[i]=1
        p,s=check(graph,i,i,depth,visited,sum,best_ans,[i])
        if s<best_ans:
            best_ans=s
            best_path=p[:]



    if best_ans == INF:
        return (-1, [])
    return (best_ans, [x + 1 for x in best_path])



def check(graph,start_node,index,depth,visited,sum,best_ans,path):

    # best_ans=INF
    best_path=[]
    # sum=0
    # path+=[index]
    v=[0 for i in range(len(graph))]
    if depth==0:
        if graph[start_node][index]!=INF and sum+graph[index][start_node]<=best_ans:
            best_ans=sum+graph[start_node][index]
            best_path=path
            return best_path,best_ans
        return [],INF

    is_ok=False
    for j in range(len(graph[index])):
        if graph[index][j]!=INF and visited[j]==0 and v[j]==0:
            v[j]=1
            visited[j]=1
            path.append(j)
            if sum+graph[index][j]<=best_ans:
                p,s=check(graph,start_node,j,depth-1,visited,sum+graph[index][j],best_ans,path)
                if s !=INF and s<best_ans:
                    is_ok=True
                    best_ans=s
                    best_path=p[:]

            else:
                continue
            
            trash=path.pop()
            visited[trash]=0
    if not is_ok :
    #     trash=path.pop()
    #     visited[trash]=0
        return [],INF
    return best_path,best_ans

    

if __name__ == '__main__':
    print_answer(*optimal_path(read_data()))
