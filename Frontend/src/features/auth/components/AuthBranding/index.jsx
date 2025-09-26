import React from 'react';

export default function AuthBranding({ title, subtitle, description }) {
    return (
        <div className="block text-center md:text-left text-white max-w-[500px]">
            <h1
                className="
                    font-extrabold 
                    text-[2.5rem] sm:text-3xl md:text-[3.5rem] lg:text-4xl 
                    leading-tight mb-3 
                    bg-gradient-to-br from-white via-white to-blue-100 
                    bg-clip-text text-transparent
                    [text-shadow:0_4px_20px_rgba(255,255,255,0.3)]
                "
            >
                {title}
            </h1>
            <h5
                className="
                    font-light 
                    text-[1.1rem] sm:text-[1.3rem] md:text-[1.5rem] lg:text-[1.75rem] 
                    leading-snug mb-2 
                    text-[#f8f9fa] opacity-95
                "
            >
                {subtitle}
            </h5>
            <p
                className="
                    font-light 
                    text-[0.95rem] sm:text-base md:text-[1.1rem] 
                    leading-relaxed 
                    text-[#e1e8f0] opacity-80
                "
            >
                {description}
            </p>
        </div>
    );
}
