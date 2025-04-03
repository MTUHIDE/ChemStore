import uvicorn


def rundev():
    uvicorn.run("chemstore.asgi:application", log_level="debug", reload=True)
