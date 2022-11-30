#python3

import queue

def create_result(path,paths):
    ans=[]
    for i in range(1,len(path)):
        ans.append(path[i])
        j=0
        while(True):
            if j>=len(paths):
                break
            if path[i]==paths[j][0]:
                current_path=paths.pop(j)
                j-=1
                ans+=create_result(current_path,paths)
            j+=1
    return ans

def find_path(adj,outputs,index,q):
    path=[index]
    last_node=index
    while(True):
        outputs[last_node]-=1
        current_node=adj[last_node][-1]
        q.put(current_node)
        adj[last_node].remove(current_node)
        path.append(current_node)
        if current_node==index:
            return path
        last_node=current_node



def Eulerian_cycle(adj,outputs):
    paths=[]
    q=queue.Queue()
    q.put(0)
    while(not q.empty()):
        i=q.get()
        if outputs[i]>0:
            paths.append(find_path(adj,outputs,i,q))
            continue
    
    ans=[]    
    path=paths.pop(0)
    ans+=[path[0]]+ create_result(path,paths)
    


m=int(input())
n=2**m
adj=[]
# outputs=[]
used=[]
for i in range(n):
    used.append(0)
    # if i==0 or i==n-1:
    #     outputs.append(1)
    # else:
    #     outputs.append(2)
    adj.append([])
    if i%2==0:
        
        if i!=0:
            adj[i].append((i*2)%n)
        adj[i].append((i*2)%n+1)
    else:
        
        adj[i].append((i*2)%n)
        if i!=n-1:
            adj[i].append((i*2)%n+1)

# print(Eulerian_cycle(adj,outputs))





# path=[]
# count=0
# v={}
# for i in adj[0]:
#     v[i]=False
# path.append([0,v])
# is_finished=False
# while(True):
#     item=path[-1]
#     current_node=item[0]
#     visited=item[1]
#     isChosen=False
#     for i in adj[current_node]:
#         if not isChosen:
#             if i==0:
#                 if count==n-1:
#                     is_finished=True
#                     break
#                 else:
#                     continue
#             if visited[i]==False:
#                 isExist=False
#                 for a in path:
#                     if a[0] ==i:
#                         isExist=True
#                         break
#                 if not isExist:
#                     isChosen=True
#                     visited[i]=True
#                     v={}
#                     for l in adj[i]:
#                         v[l]=False
#                     path.append([i,v])
#                     count+=1
#     if is_finished:
#         break
#     if not isChosen:
#         trash=path.pop()
#         count-=1




path=[]
count=0
v={}
for i in adj[0]:
    v[i]=False
path.append([0,v])
used[0]=1
is_finished=False
while(True):
    item=path[-1]
    current_node=item[0]
    visited=item[1]
    isChosen=False
    for i in adj[current_node]:
        if not isChosen:
            if i==0:
                if count==n-1:
                    is_finished=True
                    break
                else:
                    continue
            if visited[i]==False:
                isExist=False
                if used[i]==1:
                    isExist=True
                if not isExist:
                    isChosen=True
                    visited[i]=True
                    v={}
                    for l in adj[i]:
                        v[l]=False
                    path.append([i,v])
                    used[i]=1
                    count+=1
    if is_finished:
        break
    if not isChosen:
        trash=path.pop()
        used[trash[0]]=0
        count-=1
    


# path=[]
# count=0
# path.append(0)
# is_finished=False
# while(True):
#     current_node=path[-1]
#     isChosen=False
#     for i in adj[current_node]:
#         if not isChosen:
#             if i==0:
#                 if count==n-1:
#                     is_finished=True
#                     break
#                 else:
#                     continue
#             isExist=False
#             for a in path:
#                 if a[0] ==i:
#                     isExist=True
#                     break
#             if not isExist:
#                 isChosen=True
#                 visited[i]=True
#                 v={}
#                 for l in adj[i]:
#                     v[l]=False
#                 path.append([i,v])
#                 count+=1
#     if is_finished:
#         break
#     if not isChosen:
#         trash=path.pop()
#         count-=1    

result=""
for i in range(m):
    result+="0"
for i in range(1,len(path)-m+1):
    result+=str(path[i][0]%2)

print(result)