#Uses python3

import sys
import queue

def bipartite(adj):
    #write your code here
    visited=[False for i in range(len(adj))]
    order=[-1 for i in range(len(adj))]
    q=queue.Queue()
    for j in range(len(adj)):
        if visited[j]==False:
            q.put(j)
            visited[j]=True
            order[j]=0
            # reached=False
            while not q.empty():
                node=q.get()
                for i in adj[node]:
                    if visited[i]==True and order[node]%2==order[i]%2 :
                        return 0
                    if visited[i]==False:
                        visited[i]=True
                        order[i]=order[node]+1
                        # if i==t:
                        #     reached=True
                        #     break
                        q.put(i)
                # if reached:
                #     break
    return 1
    # if not reached:
    #     return -1
    # else:
    #     result=0
    #     parent=t
    #     while True:
    #         parent=parents[parent]
    #         result+=1
    #         if parent==s:
    #             return result

if __name__ == '__main__':
    input = sys.stdin.read()
    # no_of_lines = 5
    # lines = ""
    # for i in range(no_of_lines):
    #     lines+=input()+"\n"
    # input=lines
    data = list(map(int, input.split()))
    n, m = data[0:2]
    data = data[2:]
    edges = list(zip(data[0:(2 * m):2], data[1:(2 * m):2]))
    adj = [[] for _ in range(n)]
    for (a, b) in edges:
        adj[a - 1].append(b - 1)
        adj[b - 1].append(a - 1)
    print(bipartite(adj))
