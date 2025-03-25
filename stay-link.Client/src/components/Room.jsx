import { useRef, useState } from "react";
import { Html } from "@react-three/drei";
import { useFrame } from "@react-three/fiber";

export default function Room({ position, name, data }) {
  const [hovered, setHovered] = useState(false);

  return (
    <mesh
      position={position}
      onPointerOver={() => setHovered(true)}
      onPointerOut={() => setHovered(false)}
    >
      <boxGeometry args={[2, 2, 2]} />
      <meshStandardMaterial color={hovered ? "orange" : "skyblue"} />
      {hovered && (
        <Html distanceFactor={10}>
          <div
            style={{
              background: "rgba(255,255,255,0.8)",
              padding: "4px 8px",
              borderRadius: "6px",
              fontSize: "12px",
              whiteSpace: "nowrap",
            }}
          >
            <strong>{name}</strong>
            <br />
            Užimtumas: {data.usage}%<br />
            Valymai: {data.cleanings}
          </div>
        </Html>
      )}
    </mesh>
  );
}
