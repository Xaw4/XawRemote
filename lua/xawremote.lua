print("Starting xawremote... ")
serverUrl = "http://localhost:8888/"

function sendRequest(retValue)
        local label = os.getComputerLabel()
		retValue = retValue or "null"
		local request = serverUrl..label.."?param="..retValue
		print("-- send request:")
		print(request)
		http.request(request)
end
 
function wait()
        print("nothing to do here .. ")
        os.sleep(1)
end
 
function waitForAnswer()
        while true do
                local event, host, params = os.pullEvent()
				print(params)
                if event == "http_failure" then
                        wait()
                        sendRequest("waiting")
                else
                   if event == "http_success" then
				   
						output = "success"
                        if program == "wait" then
                                wait()
                        else
							param = params.readAll()
							-- print ("host: "..host.." params: "..param)
							f = assert(loadstring(param, output))
							
							local status = pcall(f)
							if status then 
								print("successfully called f")
							else
								output = "error"
							end
							print("output")
                        end
                        sendRequest(output)
                  end
                end
        end
end
 
sendRequest("first")
waitForAnswer()