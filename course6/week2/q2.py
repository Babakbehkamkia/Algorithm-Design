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
    path=[index+1]
    last_node=index
    while(True):
        outputs[last_node]-=1
        current_node=adj[last_node][-1]
        q.put(current_node)
        adj[last_node].remove(current_node)
        path.append(current_node+1)
        if current_node==index:
            return path
        last_node=current_node



def Eulerian_cycle(n,adj,inputs,outputs):
    for i in range(n):
        if inputs[i]!=outputs[i]:
            return 0
    
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

    result="1\n"
    for i in range(len(ans)-2):
        result+=str(ans[i])+" "
    result+=str(ans[-2])

    return result






n,m=map(int,input().split())
adj=[]
inputs=[]
outputs=[]
for i in range(n):
    adj.append([])
    inputs.append(0)
    outputs.append(0)

for i in range(m):
    left,right=map(int,input().split())
    adj[left-1].append(right-1)
    inputs[right-1]+=1
    outputs[left-1]+=1

print(Eulerian_cycle(n,adj,inputs,outputs))