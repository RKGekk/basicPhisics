using ge.MathPrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge.Engine {
    public class Camera {
        public enum Camera_Movement {
            FORWARD,
            BACKWARD,
            LEFT,
            RIGHT
        }

        private const float ONEDEGRAD = (float)(Math.PI / 180.0);
        private const float YAW = -90.0f;
        private const float PITCH = 0.0f;
        private const float SPEED = 10.0f;
        private const float SENSITIVTY = 0.1f;
        private const float ZOOM = 90.0f;

        private Vec3 Position;
	    private Vec3 Front;
	    private Vec3 Up;
	    private Vec3 Right;
	    private Vec3 WorldUp;

	    private float Yaw;
	    private float Pitch;

	    private float MovementSpeed;
	    private float MouseSensitivity;
	    private float Zoom;

        public Camera(Vec3 position, Vec3 up, float yaw = YAW, float pitch = PITCH) {
            Front = new Vec3(0.0f, 0.0f, -1.0f);
            MovementSpeed = SPEED;
            MouseSensitivity = SENSITIVTY;
            Zoom = ZOOM;
	    	Position = position;
	    	WorldUp = up;
	    	Yaw = yaw;
	    	Pitch = pitch;
	    	updateCameraVectors();
	    }

        public Camera(float posX, float posY, float posZ, float upX, float upY, float upZ, float yaw, float pitch) {
            Front = new Vec3(0.0f, 0.0f, -1.0f);
            MovementSpeed = SPEED;
            MouseSensitivity = SENSITIVTY;
            Zoom = ZOOM;
	    	Position = new Vec3(posX, posY, posZ);
	    	WorldUp = new Vec3(upX, upY, upZ);
	    	Yaw = yaw;
	    	Pitch = pitch;
	    	updateCameraVectors();
	    }

        public Mat4 GetViewMatrix() {
	    	return Mat4.lookAtRH(Position, Position + Front, Up);
	    }

        public void ProcessKeyboard(Camera_Movement direction, float deltaTime) {
	    	float velocity = MovementSpeed * deltaTime;
	    	if (direction == Camera_Movement.FORWARD)
	    		Position += Front * velocity;
	    	if (direction == Camera_Movement.BACKWARD)
	    		Position -= Front * velocity;
	    	if (direction == Camera_Movement.LEFT)
	    		Position -= Right * velocity;
	    	if (direction == Camera_Movement.RIGHT)
	    		Position += Right * velocity;
	    }

        public void ProcessKeyboard(Vec3 direction, float deltaTime) {
	    	float velocity = MovementSpeed * deltaTime;
	    	Position += direction * velocity;
	    }

        public void ProcessMouseMovement(float xoffset, float yoffset, bool constrainPitch = true) {
	    	xoffset *= MouseSensitivity;
	    	yoffset *= MouseSensitivity;

	    	Yaw += xoffset;
	    	Pitch += yoffset;

	    	if (constrainPitch) {
	    		if (Pitch > 89.0f)
	    			Pitch = 89.0f;
	    		if (Pitch < -89.0f)
	    			Pitch = -89.0f;
	    	}

	    	updateCameraVectors();
	    }

        public void ProcessMouseScroll(float yoffset) {
	    	if (Zoom >= 1.0f && Zoom <= 45.0f)
	    		Zoom -= yoffset;
	    	if (Zoom <= 1.0f)
	    		Zoom = 1.0f;
	    	if (Zoom >= 45.0f)
	    		Zoom = 45.0f;
	    }

        public void updateCameraVectors() {

	    	Vec3 front = new Vec3();
	    	front.x = (float)(Math.Cos(Yaw * ONEDEGRAD) * Math.Cos(Pitch * ONEDEGRAD));
	    	front.y = (float)(Math.Sin(Pitch * ONEDEGRAD));
	    	front.z = (float)(Math.Sin(Yaw * ONEDEGRAD) * Math.Cos(Pitch * ONEDEGRAD));
	    	Front = front.normalize();

	    	Right = (Front * WorldUp).normal();
	    	Up = (Right * Front).normal();
	    }
    }
}
