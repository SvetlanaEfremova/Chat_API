## Chat API Endpoints

### 1. Create a Chat
- **Type**: POST
- **Endpoint**: `/api/chat`
- **Body**: 
  ```json
  {
    "name": "Name of the Chat"
  }
- **Query Parameters**:
userId: ID of the user creating the chat.

### 2. Retrieve All Chats:
- **Type**: GET
- **Endpoint**: `/api/chat`

### 3. Retrieve a Specific Chat by ID:
- **Type**: GET
- **Endpoint**: `/api/chat/{id}`
- **Note**: Where {id} is the ID of the chat.

### 4. Update a Chat:
- **Type**: PUT
- **Endpoint**: /api/chat/{id}
- **Note**: Where {id} is the ID of the chat.
- **Body**:
  ```json
  {
    "id": "{id}",
    "name": "Updated Name of the Chat"
  }
- **Query Parameters**:
 userId: ID of the user updating the chat.

### 5. Delete a Chat:
- **Type**: DELETE
- **Endpoint**: /api/chat/{id}
- **Note**: Where {id} is the ID of the chat.
- **Query Parameters**:
 userId: ID of the user deleting the chat.
  
## 6. Send a Message to a Chat:
- **Type**: POST
- **Endpoint**: /api/chat/{chatId}/messages
- **Note**: Where {chatId} is the ID of the chat.
- **Body**:
  ```json
  {
    "text": "Message Text",
    "userId": "ID of the User Sending the Message"
  }

## 7. Retrieve Messages from a Chat:
- **Type**: GET
- **Endpoint**: /api/chat/{chatId}/messages
- **Note**: Where {chatId} is the ID of the chat.
